using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.Commerce.Domain.Model.Aggregates;
using SweetManagerWebService.Commerce.Domain.Model.Commands.Subscriptions;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Subscriptions;
using SweetManagerWebService.Commerce.Domain.Services.Subscriptions;
using SweetManagerWebService.Commerce.Interfaces.REST;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class SubscriptionControllerTests
{
    // ✅ Test 1: Crear suscripción correctamente
    [Test]
    public async Task CreateSubscription_ReturnsOk()
    {
        var mockCommand = new Mock<ISubscriptionCommandService>();
        var mockQuery = new Mock<ISubscriptionQueryService>();

        var resource = new CreateSubscriptionResource("Premium", "Monthly plan", 99.99m, "active");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateSubscriptionCommand>())).ReturnsAsync(true);

        var controller = new SubscriptionController(mockCommand.Object, mockQuery.Object);

        var result = await controller.CreateSubscription(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.EqualTo("Subscription created correctly!"));
    }

    

    // ✅ Test 3: Obtener todas las suscripciones correctamente
    [Test]
    public async Task GetAllSubscriptions_ReturnsList()
    {
        var mockCommand = new Mock<ISubscriptionCommandService>();
        var mockQuery = new Mock<ISubscriptionQueryService>();

        var subscriptions = new List<Subscription>
        {
            new Subscription("Basic", "Entry plan", 30.00m, "active"),
            new Subscription("Premium", "Advanced plan", 120.00m, "inactive")
        };

        mockQuery.Setup(s => s.Handle(It.IsAny<GetAllSubscriptionsQuery>())).ReturnsAsync(subscriptions);

        var controller = new SubscriptionController(mockCommand.Object, mockQuery.Object);

        var result = await controller.GetAllSubscriptions();

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<SubscriptionResource>;
        Assert.That(list.Count(), Is.EqualTo(2));
    }

    

    // ✅ Test 5: Obtener suscripción por ID correctamente
    [Test]
    public async Task GetSubscriptionById_ReturnsCorrectSubscription()
    {
        var mockCommand = new Mock<ISubscriptionCommandService>();
        var mockQuery = new Mock<ISubscriptionQueryService>();

        var subscription = new Subscription("Pro", "Full plan", 199.99m, "active");

        mockQuery.Setup(s => s.Handle(It.Is<GetSubscriptionByIdQuery>(q => q.Id == 10)))
                 .ReturnsAsync(subscription);

        var controller = new SubscriptionController(mockCommand.Object, mockQuery.Object);

        var result = await controller.GetSubscriptionById(10);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var resource = ((OkObjectResult)result).Value as SubscriptionResource;
        Assert.That(resource.Name, Is.EqualTo("PRO")); // Recuerda que Name se guarda en mayúsculas
        Assert.That(resource.Price, Is.EqualTo(199.99m));
    }
}
