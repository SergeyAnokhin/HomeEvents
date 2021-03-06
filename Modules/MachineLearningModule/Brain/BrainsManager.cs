﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Config;
using MachineLearningModule.Brain.Model;
using MachineLearningModule.Brain.Services;
using MachineLearningModule.Events;

namespace MachineLearningModule.Brain
{
    public class BrainsManager : IBrainsManager
    {
        private readonly List<IBrainApiAdapter> brainApiAdapters;
        private ILogService log;
        private Config.Config config;

        public BrainsManager(ILogService logService, IBrainApiAdapter[] brainApiAdapters, IAppConfigService configService)
        {
            config = configService.GetModuleConfig<Config.Config>();
            this.brainApiAdapters = brainApiAdapters
                .Where(a =>  a.IsActive())
                .ToList();
            if (this.brainApiAdapters.Count == 0)
            {
                throw new NullReferenceException("No active IBrainApiAdapter found. Registered : " + brainApiAdapters.StringJoin());
            }
            log = logService.Init(GetType());
        }

        public IEnumerable<BrainInfo> GetActiveBrains()
        {
            return brainApiAdapters.Select(a => a.GetBrainInfo());
        }

        public IEnumerable<ClassificationPrediction> Predict(List<HomeEvent> events)
        {
            return brainApiAdapters.Select(a => a.Predict(new ClassificationInputData(events)));
        }

        public IEnumerable<ClassificationPrediction> AddToModel(IEnumerable<HomeEvent> events, string className)
        {
            return brainApiAdapters
                .SelectMany(a => a.Fit(new List<ClassificationInputData>
                {
                    new ClassificationInputData(events, className)
                }).ToList());
        }

        public IEnumerable<ClassificationPrediction> FitModel(IEnumerable<ClassificationInputData> data)
        {
            return brainApiAdapters
                .SelectMany(a => a.Fit(data).ToList()).ToList();
        }

        public IEnumerable<ClassificationPrediction> FitModel()
        {
            // load from MongoDB
            throw new NotImplementedException();
        }
    }
}
