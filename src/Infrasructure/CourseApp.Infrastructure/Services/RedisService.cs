﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseApp.Cache {
    public class RedisService {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        public RedisService(string url)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(url);
        }

        public IDatabase GetDatabase(int index) {
           return _connectionMultiplexer.GetDatabase(index);
        }
    }
}
