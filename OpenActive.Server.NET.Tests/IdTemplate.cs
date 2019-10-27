using System;
using Xunit;
using OpenActive.Server.NET;

namespace OpenActive.Server.NET.Tests
{
    public class IdTemplate
    {
        public class SessionSeriesComponents
        {
            public string BaseUrl { get; set; }
            public string EventType { get; set; }
            public string SessionSeriesId { get; set; }
            public long? ScheduledSessionId { get; set; }
            public long? OfferId { get; set; }
        }

        [Fact]
        public void SingleIdTemplate_GetIdComponents()
        {
            var template = new SingleIdTemplate<SessionSeriesComponents>(
                "{+BaseUrl}api/{EventType}/{SessionSeriesId}/events/{ScheduledSessionId}"
                );

            var components = template.GetIdComponents(new Uri("https://example.com/api/session-series/asdf/events/123"));

            Assert.Equal("https://example.com/", components.BaseUrl);
            Assert.Equal("session-series", components.EventType);
            Assert.Equal("asdf", components.SessionSeriesId);
            Assert.Equal(123, components.ScheduledSessionId);
        }

        [Fact]
        public void SingleIdTemplate_RenderId()
        {
            var template = new SingleIdTemplate<SessionSeriesComponents>(
                "{+BaseUrl}api/{EventType}/{SessionSeriesId}/events/{ScheduledSessionId}"
                );

            var uri = new Uri("https://example.com/api/session-series/asdf/events/123");

            var components = template.GetIdComponents(uri);

            var outputUri = template.RenderId(components);
            
            Assert.Equal(uri, outputUri);
        }

        [Fact]
        public void BookablePairIdTemplate_GetIdComponents()
        {
            var template = new BookablePairIdTemplate<SessionSeriesComponents>(
                "{+BaseUrl}api/{EventType}/{SessionSeriesId}/events/{ScheduledSessionId}",
                "{+BaseUrl}api/{EventType}/{SessionSeriesId}/events/{ScheduledSessionId}#/offers/{OfferId}"
                );

            var components = template.GetIdComponents(
                new Uri("https://example.com/api/session-series/asdf/events/123"),
                new Uri("https://example.com/api/session-series/asdf/events/123#/offers/456")
                );

            Assert.Equal("https://example.com/", components.BaseUrl);
            Assert.Equal("session-series", components.EventType);
            Assert.Equal("asdf", components.SessionSeriesId);
            Assert.Equal(123, components.ScheduledSessionId);
            Assert.Equal(456, components.OfferId);

            Assert.Throws<BookableOpportunityAndOfferMismatchException>(() => template.GetIdComponents(
                new Uri("https://example.com/api/session-series/asdf/events/123"),
                new Uri("https://example.com/api/session-series/asdf/events/124#/offers/456")
                ));

            Assert.Throws<BookableOpportunityAndOfferMismatchException>(() => template.GetIdComponents(
                new Uri("https://example.com/api/session-series/asdf/events/123"),
                new Uri("https://example.com/api/session-series/asdF/events/123#/offers/456")
                ));
        }
    }
}