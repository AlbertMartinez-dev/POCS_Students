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

        [Fact]
        public void Create_WithEndBeforeStart_ReturnsConflictError()
        {

            var start = new DateTime(2026, 6, 1);
            var end = new DateTime(2026, 5, 30);

            var result = DateRange.Create(start, end);

            result.IsError.Should().BeTrue();
            result.Errors.Should().Contain(error => error.Code == "DateRange.Conflict");


        }
        [Fact]
        public void Create_EndWithSameStart_ReturnsConflictError()
        {
            var start = new DateTime(2026, 6, 1);
            var end = new DateTime(2026, 6, 1);

            var result = DateRange.Create(start, end);

            result.IsError.Should().BeTrue();
            result.Errors.Should().Contain(error => error.Code == "DateRange.Conflict");
        }

        [Fact]

        public void Nights_WithValidDateRange_ReturnsNumberOfNights()
        {
            var start = new DateTime(2026, 6, 1);
            var end = new DateTime(2026, 6, 5);

            // Act

            var result = DateRange.Create(start, end);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Nights.Should().Be(4);

        }
        
        [Fact]

        public void Overlaps_WhenDateRangesOverlap_ReturnsTrue()
        {
            var first = DateRange.Create(
                new DateTime(2026, 6, 1),
                new DateTime(2026, 6, 5)
                ).Value;

            var second = DateRange.Create(
                new DateTime(2026, 6, 4),
                new DateTime(2026, 6, 8)
                ).Value;



            var result = first.Overlaps(second);

            result.Should().BeTrue();

        }


        public void Overlaps_WhenDateRangesDontOverlap_ReturnsFalse()
        {
            var first = DateRange.Create(
                new DateTime(2026, 6, 1),
                new DateTime(2026, 6, 5)
                ).Value;

            var second = DateRange.Create(
                new DateTime(2026, 6, 6),
                new DateTime(2026, 6, 8)
                ).Value;



            var result = first.Overlaps(second);

            result.Should().BeFalse();
        }














    }
}
