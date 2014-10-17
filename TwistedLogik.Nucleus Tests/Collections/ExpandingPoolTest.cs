﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwistedLogik.Nucleus.Collections;
using TwistedLogik.Nucleus.Testing;

namespace TwistedLogik.Nucleus.Tests.IO
{
    [TestClass]
    public class ExpandingPoolTest : NucleusTestFramework
    {
        [TestMethod]
        public void ExpandingPool_ShouldUseSpecifiedObjectInitializers()
        {
            var pool = CreateExpandingPoolWithCapacity(5, () => { return new List<Int32>() { 9, 8, 7, 6, 5, 4, 3, 2, 1 }; });

            var item = pool.Retrieve();

            TheResultingCollection(item)
                .ShouldBeExactly(9, 8, 7, 6, 5, 4, 3, 2, 1);
        }

        [TestMethod]
        public void ExpandingPool_ExpandsWhenCapacityIsExceeded()
        {
            var pool = CreateExpandingPoolWithCapacity(5);
            
            var retrieved = 0;
            var allocated = new List<List<Int32>>();
            for (int i = 0; i < 10; i++)
            {
                allocated.Add(pool.Retrieve());
                retrieved++;
            }

            TheResultingValue(retrieved).ShouldBe(10);
        }

        [TestMethod]
        public void ExpandingPool_ShouldBeCleanedUpAfterObjectsAreReleased()
        {
            var pool = CreateExpandingPoolWithCapacity(5);

            var allocated = new List<List<Int32>>();
            for (int i = 0; i < 10; i++)
            {
                allocated.Add(pool.Retrieve());
            }

            foreach (var item in allocated)
            {
                pool.Release(item);
            }

            TheResultingValue(pool.Count).ShouldBe(0);
        }

        private ExpandingPool<List<Int32>> CreateExpandingPoolWithCapacity(Int32 capacity, Func<List<Int32>> allocator = null)
        {
            return new ExpandingPool<List<Int32>>(capacity, allocator);
        }
    }
}
