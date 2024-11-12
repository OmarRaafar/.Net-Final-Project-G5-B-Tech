using ApplicationB.Services_B.Order;
using ApplicationB.Services_B.Product;
using AutoMapper;
using DTOsB.Order.OrderDTO;
using DTOsB.Order.OrderItemDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B_Tech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IProductTranslationService productTranslationService;
        private readonly IOrderItemService orderItemService;
        private IMapper mapper;

        public OrderController(IOrderService _orderService, IProductService _productService,
            IProductTranslationService _productTranslationService, IOrderItemService _orderItemService,
            IMapper _mapper)
        {
            orderService = _orderService;
            productService = _productService;
            productTranslationService = _productTranslationService;
            orderItemService = _orderItemService;
            mapper = _mapper;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //**********************************************************

        [HttpPut("update-order-item-quantity")]
        public async Task<IActionResult> UpdateOrderItemQuantity(int orderItemId, int newQuantity)
        {
            var orderItem = (await orderItemService.GetOrderItemByIdAsync(orderItemId)).Entity;

            if (orderItem == null || orderItem.IsDeleted == true)
            {
                return NotFound("Order item not found.");
            }

            // Check if there’s enough stock
            var product = (await productService.GetProductByIdAsync(orderItem.ProductId)).Entity;

            if (product.StockQuantity < newQuantity)
            {
                return BadRequest("Not enough stock available.");
            }

            // Update quantity
            AddOrUpdateOrderItemBDTO updatedOrderItem = mapper.Map<AddOrUpdateOrderItemBDTO>(orderItem);
            updatedOrderItem.Quantity = newQuantity;
            await orderItemService.UpdateOrderItemAsync(updatedOrderItem);

            return Ok(new { message = "Order item quantity updated successfully." });
        }


        //**********************************************************

        [HttpDelete("cancel-order/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var order = await orderService.GetOrderByIdAsync(orderId);

            if (order == null || order.IsDeleted == true)
            {
                return NotFound("Order not found.");
            }

            await orderService.DeleteOrderAsync(orderId);

            return Ok(new { message = "Order and all related items have been canceled." });
        }

        //*******************************************************

        [HttpDelete("order-item/{orderItemId}")]
        public async Task<IActionResult> DeleteOrderItem(int orderItemId)
        {
            var orderItem = (await orderItemService.GetOrderItemByIdAsync(orderItemId)).Entity;

            if (orderItem == null || orderItem.IsDeleted == true)
            {
                return NotFound(new { message = "Order item not found." });
            }

            await orderItemService.DeleteOrderItemAsync(orderItemId);

            return Ok(new { message = "Order item deleted successfully." });
        }



        //***************************************************************

        [HttpPost("add-to-cart")]
        //public async Task<IActionResult> AddToCart(int productId, string userId)
        //{
        //    Find an existing "InCart" order for the user

        //   var order = (await orderService.GetAllOrdersAsync()).FirstOrDefault(p => p.CurrentStatus == ModelsB.Order_B.Status.InCart && p.ApplicationUserId == userId);
        //    var product = (await productService.GetProductByIdAsync(productId)).Entity;
        //    var productTranslation = (await productTranslationService.GetTranslationsByProductIdAsync(productId)).Entity?.FirstOrDefault();
        //    AddOrUpdateOrderBDTO addOrder;
        //    if (order == null)
        //    {
        //        addOrder = new AddOrUpdateOrderBDTO()
        //        {
        //            CurrentStatus = ModelsB.Order_B.Status.InCart,
        //            TotalPrice = product.Price,
        //            OrderDate = DateTime.Now,
        //            ApplicationUserId = userId,
        //            CreatedBy = userId,
        //            UpdatedBy = userId,
        //        };
        //        await orderService.CreateOrderAsync(addOrder);
        //        order = (await orderService.GetAllOrdersAsync()).FirstOrDefault(p => p.CurrentStatus == ModelsB.Order_B.Status.InCart && p.ApplicationUserId == userId);
        //    }
        //    else
        //    {
        //        addOrder = new AddOrUpdateOrderBDTO()
        //        {
        //            Id = order.Id,
        //            CurrentStatus = ModelsB.Order_B.Status.InCart,
        //            TotalPrice = product.Price + order.TotalPrice,
        //            OrderDate = order.OrderDate,
        //            ApplicationUserId = userId,
        //            UpdatedBy = userId,
        //            CreatedBy = userId,
        //            orderItems = (List<AddOrUpdateOrderItemBDTO>)order.OrderItems,
        //        };
        //        await orderService.UpdateOrderAsync(addOrder);
        //    }
        //    order = await orderService.GetOrderByIdAsync(order.Id);

        //    Check if the product is already in the cart

        //    string pr = "Product One";
        //    SelectOrderItemBDTO orderItem;
        //    if (order == null) orderItem = new SelectOrderItemBDTO();
        //    else orderItem = order.OrderItems
        //        .FirstOrDefault(oi => oi.ProductId == productId);

        //    if (orderItem != null)
        //    {
        //        Update the quantity if the product is already in the cart
        //        var newOrderItem = new AddOrUpdateOrderItemBDTO()
        //        {
        //            Id = orderItem.Id,
        //            ProductId = productId,
        //            Quantity = orderItem.Quantity + 1,
        //            OrderId = order.Id,
        //            UpdatedBy = userId,
        //            CreatedBy = userId,
        //        };
        //        await orderItemService.UpdateOrderItemAsync(newOrderItem);
        //    }
        //    else
        //    {
        //        Add a new OrderItem for the product

        //       var newOrderItem = new AddOrUpdateOrderItemBDTO()
        //       {
        //           ProductId = productId,
        //           Quantity = 1,
        //           OrderId = order.Id,
        //           CreatedBy = userId,
        //           UpdatedBy = userId,
        //       };
        //        await orderItemService.CreateOrderItemAsync(newOrderItem);

        //    }

        //    return Ok(new { message = "Product added successfully" });
        //}


        public async Task<IActionResult> AddToCart(int productId, string userId)
        {
            try
            {
                // استرجاع الطلب الحالي "InCart" للمستخدم، أو إنشاء طلب جديد إذا لم يكن موجودًا
                var orders = await orderService.GetAllOrdersAsync();
                var order = orders.FirstOrDefault(p => p.CurrentStatus == ModelsB.Order_B.Status.InCart && p.ApplicationUserId == userId);

                // جلب تفاصيل المنتج
                var productResponse = await productService.GetProductByIdAsync(productId);
                if (productResponse?.Entity == null)
                {
                    return NotFound(new { message = "Product not found" });
                }
                var product = productResponse.Entity;

                // جلب تفاصيل الترجمة (اختياري حسب الحاجة)
                var productTranslation = (await productTranslationService.GetTranslationsByProductIdAsync(productId)).Entity?.FirstOrDefault();

                AddOrUpdateOrderBDTO addOrder;
                if (order == null)
                {
                    // إنشاء طلب جديد إذا لم يكن هناك طلب "InCart"
                    addOrder = new AddOrUpdateOrderBDTO()
                    {
                        CurrentStatus = ModelsB.Order_B.Status.InCart,
                        TotalPrice = product.Price,
                        OrderDate = DateTime.Now,
                        ApplicationUserId = userId,
                        CreatedBy = userId,
                        UpdatedBy = userId,
                    };
                    await orderService.CreateOrderAsync(addOrder);

                    // تحديث الطلب بعد الإنشاء
                    orders = await orderService.GetAllOrdersAsync();
                    order = orders.FirstOrDefault(p => p.CurrentStatus == ModelsB.Order_B.Status.InCart && p.ApplicationUserId == userId);
                }
                else
                {
                    // تحديث الطلب الحالي إذا كان موجودًا
                    addOrder = new AddOrUpdateOrderBDTO()
                    {
                        Id = order.Id,
                        CurrentStatus = ModelsB.Order_B.Status.InCart,
                        TotalPrice = product.Price + order.TotalPrice,
                        OrderDate = order.OrderDate,
                        ApplicationUserId = userId,
                    };
                    await orderService.UpdateOrderAsync(addOrder);
                }

                if (order == null)
                {
                    return BadRequest(new { message = "Failed to create or retrieve order" });
                }

                // التحقق من وجود المنتج بالفعل في عناصر الطلب
                var orderItem = order.OrderItems?.FirstOrDefault(oi => oi.ProductId == productId);

                if (orderItem != null)
                {
                    // تحديث الكمية إذا كان المنتج موجودًا في السلة
                    var updatedOrderItem = new AddOrUpdateOrderItemBDTO()
                    {
                        Id = orderItem.Id,
                        ProductId = productId,
                        Quantity = orderItem.Quantity + 1,
                        OrderId = order.Id,
                    };
                    await orderItemService.UpdateOrderItemAsync(updatedOrderItem);
                }
                else
                {
                    // إضافة عنصر جديد إذا لم يكن المنتج موجودًا في السلة
                    var newOrderItem = new AddOrUpdateOrderItemBDTO()
                    {
                        ProductId = productId,
                        Quantity = 1,
                        OrderId = order.Id,
                    };
                    await orderItemService.CreateOrderItemAsync(newOrderItem);
                }

                return Ok(new { message = "Product added successfully" });
            }
            catch (Exception ex)
            {
                // معالجة الأخطاء وإرجاع استجابة مناسبة
                return StatusCode(500, new { message = "An error occurred while adding the product to the cart", error = ex.Message });
            }
        }


        //****************************************************************************

        [HttpGet]
        public async Task<IActionResult> ViewCart(string userId)
        {
            // Retrieve the "InCart" order for the user
            var order = (await orderService.GetAllOrdersAsync()).FirstOrDefault(p => p.CurrentStatus == ModelsB.Order_B.Status.InCart && p.ApplicationUserId == userId);

            if (order == null || order.IsDeleted == true)
            {
                return NotFound("You have no products in the cart.");
            }
            var inCartOrder = await orderService.GetOrderByIdAsync(order.Id);

            // Map OrderItems to a response DTO if needed
            var cartItems = inCartOrder.OrderItems.Select(oi => new
            {
                Id = oi.Id,
                Quantity = oi.Quantity,
                ProductName = oi.ProductName,
                ProductPrice = oi.Price,
                TotalPrice = oi.TotalPrice,
                StockQuantity = oi.StockQuantity,
                imageUrl = oi.Url,
                orderId = order.Id
            }).ToList();

            return Ok(cartItems);
        }

        [HttpPost("finish-order")]
        public async Task<IActionResult> FinishOrder(int orderId, decimal total, string user)
        {
            try
            {
                // Retrieve the order by ID
                var order = await orderService.GetOrderByIdAsync(orderId);
                //var order = orderResponse?.OrderItems;

                //if (order == null || order.IsDeleted)
                //{
                //    return NotFound(new { message = "Order not found or has been deleted." });
                //}

                // Update order details: status, total, and user who completed the order
                order.CurrentStatus = ModelsB.Order_B.Status.Delivered; // Assuming "Completed" status exists
                order.TotalPrice = total;
                order.UpdatedBy = user;
                order.Updated = DateTime.Now;

                // Map updated order to DTO and save changes
                var updatedOrderDTO = mapper.Map<AddOrUpdateOrderBDTO>(order);
                await orderService.UpdateOrderAsync(updatedOrderDTO);

                return Ok(new { message = "Order finished successfully." });
            }
            catch (Exception ex)
            {
                // Handle errors and return a suitable response
                return StatusCode(500, new { message = "An error occurred while finishing the order.", error = ex.Message });
            }
        }


    }
}
