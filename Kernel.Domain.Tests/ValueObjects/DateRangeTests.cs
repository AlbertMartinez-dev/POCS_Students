using System;
using System.Collections.Generic;
using System.Text;
using Kernel.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Kernel.Domain.Tests.ValueObjects
{
    public class DateRangeTests
    {

        [Fact]


        public void Create_WithValidDates_ReturnsDateRange()
        {
            // arrange
            var start = new DateTime(2026, 6, 1);
            var end = new DateTime(2026, 6, 5);

            // Act

            var result = DateRange.Create(start, end);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Start.Should().Be(start.Date);
            result.Value.End.Should().Be(end.Date);
        }
        









    }
}
