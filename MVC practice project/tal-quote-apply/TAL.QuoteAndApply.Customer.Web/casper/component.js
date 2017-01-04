// Component testing
// =================

// Takes a screenshot of 
// every component defined in configs/moduleScreenshots 
// in every breakpoints defined in configs/breakpoints

var phantomcss = require( 'phantomcss' );
var config = require('configs/config');
var _ = require('underscore');
var breakpoints = require('configs/breakpoints');
var modulesTotest = require('configs/moduleScreenshots');


phantomcss.init( config );

function snapBreakpoint (breakpoints, callback) {
	_.each(breakpoints, function(breakpoint){
		casper.then(function(){
			casper.viewport( breakpoint[0],  breakpoint[1] );
		});
		callback(breakpoint);
	});
}


casper.on( 'remote.message', function ( msg ) {
	this.echo( msg );
} )

casper.on( 'error', function ( err ) {
	this.die( "PhantomJS has an error: " + err );
} );

casper.on( 'resource.error', function ( err ) {
	casper.log( 'Resource load error: ' + err, 'warning' );
} );

// Run tests for each view.
_.each(modulesTotest, function(view) {

	//phantomcss.turnOffAnimations();
	casper.test.begin( 'Testing Quote & Apply: ' + view.name, function ( test ) {
		
		/*
			The test scenario
		*/
		casper.start(view.url);
		snapBreakpoint(breakpoints, function(breakpoint){
			casper.then( function () {
				phantomcss.screenshot( view.selector, view.name + '/' + breakpoint[0] + 'x' + breakpoint[1] );
			});
		});
		if(_.isArray(view.shots)){
			_.each(view.shots, function(item, index){
				casper.then( function () {
					item.setup();
				});
				snapBreakpoint(breakpoints, function(breakpoint){
					casper.then( function () {
						phantomcss.screenshot( view.selector, view.name + '/step'+ index + '/' + breakpoint[0] + 'x' + breakpoint[1] );
					});
				});
			});
		} else{
			console.log('no shots');
		}
		
		
		casper.then( function now_check_the_screenshots() {
			// compare screenshots
			phantomcss.compareAll();
		} );

		/*
		Casper runs tests
		*/
		casper.run( function () {
			console.log( '\nTHE END.' );
			// phantomcss.getExitStatus() // pass or fail?
			casper.test.done();
		} );

	});
});