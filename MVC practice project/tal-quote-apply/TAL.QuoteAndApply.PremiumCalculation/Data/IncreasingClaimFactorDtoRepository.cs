using System;
using System.Linq;
using TAL.QuoteAndApply.DataLayer.Factory;
using TAL.QuoteAndApply.DataLayer.Repository;
using TAL.QuoteAndApply.DataLayer.Repository.PredicateLogic;
using TAL.QuoteAndApply.DataModel.User;
using TAL.QuoteAndApply.Infrastructure;
using TAL.QuoteAndApply.Infrastructure.Cache;
using TAL.QuoteAndApply.PremiumCalculation.Configuration;
using TAL.QuoteAndApply.PremiumCalculation.Models;

namespace TAL.QuoteAndApply.PremiumCalculation.Data
{
    public interface IIncreasingClaimsFactorDtoRepository
    {
        IncreasingClaimsFactorDto GetIncreasingClaimFactor(string planCode, int brandId, bool increasingClaimsEnabled, int? benefitPeriod);
    }

    public class IncreasingClaimsFactorDtoRepository : BaseRepository<IncreasingClaimsFactorDto>, IIncreasingClaimsFactorDtoRepository
    {
        private readonly ICachingWrapper _cachingWrapper;

        public IncreasingClaimsFactorDtoRepository(IPremiumCalculationConfigurationProvider premiumCalculationConfigurationProvider, ICurrentUserProvider currentUserProvider,
            IDataLayerExceptionFactory dataLayerExceptionFactory, IDbItemEncryptionService dbItemEncryptionService, ICachingWrapper cachingWrapper)
            : base(premiumCalculationConfigurationProvider.ConnectionString, currentUserProvider, dataLayerExceptionFactory, dbItemEncryptionService)
        {
            _cachingWrapper = cachingWrapper;
        }

        public IncreasingClaimsFactorDto GetIncreasingClaimFactor(string planCode, int brandId, bool increasingClaimsEnabled, int? benefitPeriod)
        {
            var q = DbItemPredicate<IncreasingClaimsFactorDto>
                .Where(i => i.PlanCode, Op.Eq, planCode)
                .And(i=> i.BenefitPeriod, Op.Eq, benefitPeriod)
                .And(i=> i.BrandId, Op.Eq, brandId)
                .And(i => i.IncreasingClaimsEnabled, Op.Eq, increasingClaimsEnabled);

            return _cachingWrapper.GetOrAddCacheItemIndefinite(GetType(), q.PredicateKey, () => Query(q)).FirstOrDefault();
        }
    }
}