using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.Commerce.Domain.Model.Commands.Contracts;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Contracts;
using SweetManagerWebService.Commerce.Domain.Services.Contracts;
using SweetManagerWebService.Commerce.Interfaces.REST;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Contracts;
using System;
using System.Threading.Tasks;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class ContractsControllerTests
{
    // ✅ Test 1: Crear contrato correctamente
    [Test]
    public async Task CreateContractOwner_ReturnsOk()
    {
        var mockCommand = new Mock<IContractOwnerCommandService>();
        var mockQuery = new Mock<IContractOwnerQueryService>();

        var resource = new CreateContractOwnerResource(1, 2, "active");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateContractOwnerCommand>())).ReturnsAsync(true);

        var controller = new ContractsController(mockCommand.Object, mockQuery.Object);

        var result = await controller.CreateContractOwner(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.EqualTo("Contract registered correctly!"));
    }

    

    // ✅ Test 3: Obtener contrato existente por OwnerId
    [Test]
    public async Task GetContractByOwnerId_ReturnsOkWithContract()
    {
        var mockCommand = new Mock<IContractOwnerCommandService>();
        var mockQuery = new Mock<IContractOwnerQueryService>();

        var entity = new ContractOwner(1, 2, "active");

        mockQuery.Setup(s => s.Handle(It.Is<GetContractOwnerByOwnerIdQuery>(q => q.OwnerId == 2)))
                 .ReturnsAsync(entity);

        var controller = new ContractsController(mockCommand.Object, mockQuery.Object);

        var result = await controller.GetContractByOwnerId(2);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var contract = ((OkObjectResult)result).Value as ContractOwnerResource;

        Assert.That(contract.OwnersId, Is.EqualTo(2));
        Assert.That(contract.SubscriptionId, Is.EqualTo(1));
    }

    // ✅ Test 5: Cuando no se encuentra contrato
    [Test]
    public async Task GetContractByOwnerId_WhenNotFound_ReturnsBadRequest()
    {
        var mockCommand = new Mock<IContractOwnerCommandService>();
        var mockQuery = new Mock<IContractOwnerQueryService>();

        mockQuery.Setup(s => s.Handle(It.IsAny<GetContractOwnerByOwnerIdQuery>()))
                 .ReturnsAsync((ContractOwner)null);

        var controller = new ContractsController(mockCommand.Object, mockQuery.Object);

        var result = await controller.GetContractByOwnerId(99);

        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("There's no contract with the given owner id"));
    }
}
