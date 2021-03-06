﻿using System;
using System.Collections.Generic;
using Common;
using MachineLearningModule.Events;

namespace MachineLearningModule.Repositories
{
    public interface IHomeEventsService : IService
    {
        IEnumerable<HomeEvent> GetEventsWindow(DateTime dateTime);
        IEnumerable<HomeEvent> GetEvents(List<string> ids);
    }
}
