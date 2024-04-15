using Allspark.Application.UseCases.Products.DeleteProduct;
using Allspark.Web.Controllers.Products.DeleteProduct;

namespace Allspark.Web.Tests.Controllers.Products.DeleteProduct
{
    public class DeleteProductControllerTests
    {
        [Fact]
        public async Task DeleteProductAsync_ExistingProductId_ReturnsDeleteProductResponseDto()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new DeleteProductController(mediatorMock.Object);
            var productId = 1;
            var deleteProductResponseDto = new DeleteProductResponseDto { Id = productId };

            mediatorMock.Setup(mediator => mediator.Send(It.Is<DeleteProductCommand>(req => req.ProductId == productId),It.IsAny<CancellationToken>()))
                .ReturnsAsync(deleteProductResponseDto);

            // Act
            var result = await controller.DeleteProductAsync(productId);

            // Assert
            mediatorMock.Verify(mediator => mediator.Send(It.Is<DeleteProductCommand>(req => req.ProductId == productId), It.IsAny<CancellationToken>()), Times.Once);
            var responseDto = Assert.IsType<DeleteProductResponseDto>(result.Value);
            Assert.Equal(productId, responseDto.Id);
        }

        [Fact]
        public async Task DeleteProductAsync_NonExistingProductId_ReturnsNotFound()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new DeleteProductController(mediatorMock.Object);
            var productId = 1;
            DeleteProductResponseDto deleteProductResponseDto = null;

            mediatorMock.Setup(mediator => mediator.Send(It.Is<DeleteProductCommand>(req => req.ProductId == productId),It.IsAny<CancellationToken>()))
                .ReturnsAsync(deleteProductResponseDto);

            // Act
            var result = await controller.DeleteProductAsync(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
