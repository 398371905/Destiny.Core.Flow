﻿using Destiny.Core.Flow.Model.DestinyIdentityServer4;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Destiny.Core.Flow.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Destiny.Core.Flow.IdentityServer.Store
{
    public class ApiResourceStoreBase : IResourceStore
    {
        private readonly IEFCoreRepository<ApiResource, Guid> _apiResourceRepository;
        private readonly IEFCoreRepository<IdentityResource, Guid> _identityResourceRepository;
        private readonly IEFCoreRepository<ApiScope, Guid> _apiScopeRepository;
        private readonly ILogger<ApiResourceStoreBase> _logger;

        public ApiResourceStoreBase(IEFCoreRepository<ApiResource, Guid> apiResourceRepository, IEFCoreRepository<IdentityResource, Guid> identityResourceRepository, IEFCoreRepository<ApiScope, Guid> apiScopeRepository, ILogger<ApiResourceStoreBase> logger)
        {
            _apiResourceRepository = apiResourceRepository;
            _identityResourceRepository = identityResourceRepository;
            _apiScopeRepository = apiScopeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<IdentityServer4.Models.ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var model= await _apiResourceRepository.Entities.Where(x => apiResourceNames.Contains(x.Name)).ToListAsync();
            var dto=model.Select(x => x.MapTo<IdentityServer4.Models.ApiResource>()).ToList();
            return dto;
        }

        public async Task<IEnumerable<IdentityServer4.Models.ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {

            throw new NotImplementedException();

        }

        public async Task<IEnumerable<IdentityServer4.Models.ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IdentityServer4.Models.IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityServer4.Models.Resources> GetAllResourcesAsync()
        {
            var identities= await _identityResourceRepository.TrackEntities.ToListAsync();
            var scopes = await _apiScopeRepository.TrackEntities.ToListAsync();
            var apis = await _apiResourceRepository.TrackEntities.ToListAsync();
            return new IdentityServer4.Models.Resources(identities.Select(x=>x.MapTo<IdentityServer4.Models.IdentityResource>()),apis.Select(x=>x.MapTo<IdentityServer4.Models.ApiResource>()),scopes.Select(x=>x.MapTo<IdentityServer4.Models.ApiScope>()));
        }
    }
}
