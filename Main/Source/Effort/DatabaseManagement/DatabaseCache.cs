﻿#region License

// Copyright (c) 2011 Effort Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using Effort.DbCommandTreeTransform;
using Effort.Diagnostics;
using NMemory;

namespace Effort.DatabaseManagement
{
    internal class DatabaseCache : ITableProvider
    {
        private Database database;
        private ILogger logger;
        private ConcurrentDictionary<string, Expression> transformCache;
        private ConcurrentDictionary<string, Func<Dictionary<string, object>, IEnumerable>> procedureTransformCache;

        public DatabaseCache(Database database)
        {
            this.database = database;
            this.logger = new Logger();
            this.transformCache = new ConcurrentDictionary<string, Expression>();
            this.procedureTransformCache = new ConcurrentDictionary<string, Func<Dictionary<string, object>, IEnumerable>>();
        }

        public object GetTable(string name)
        {
            return database.GetTable(name);
        }

        public Database Internal
        {
            get { return this.database; }
        }

        public ILogger Logger
        {
            get { return this.logger; }
        }

        public ConcurrentDictionary<string, Expression> TransformCache
        {
            get { return this.transformCache; }
        }

        public ConcurrentDictionary<string, Func<Dictionary<string, object>, IEnumerable>> ProcedureTransformCache
        {
            get { return this.procedureTransformCache; }
        }
    }
}
