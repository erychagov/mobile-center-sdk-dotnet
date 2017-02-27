﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile.Ingestion.Models;
using System.Threading;

namespace Microsoft.Azure.Mobile.Ingestion.Http
{
    public abstract class IngestionDecorator : IIngestion
    {
        protected IIngestion DecoratedApi { get; }

        protected IngestionDecorator(IIngestion decoratedApi)
        {
            DecoratedApi = decoratedApi;
        }

        public virtual void Close()
        {
            DecoratedApi.Close();
        }

        public virtual IServiceCall PrepareServiceCall(string appSecret, Guid installId, IList<Log> logs)
        {
            return DecoratedApi.PrepareServiceCall(appSecret, installId, logs);
        }

        public virtual async Task ExecuteCallAsync(IServiceCall call)
        {
            await DecoratedApi.ExecuteCallAsync(call);
        }

        public virtual void SetServerUrl(string serverUrl)
        {
            DecoratedApi.SetServerUrl(serverUrl);
        }
    }
}