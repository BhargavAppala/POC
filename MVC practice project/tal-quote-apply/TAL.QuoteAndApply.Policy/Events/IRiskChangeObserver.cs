﻿using TAL.QuoteAndApply.Infrastructure.Observer;
using TAL.QuoteAndApply.Policy.Models;

namespace TAL.QuoteAndApply.Policy.Events
{
    public interface IRiskChangeObserver : ISimpleObserver<IRisk>
    { }
}