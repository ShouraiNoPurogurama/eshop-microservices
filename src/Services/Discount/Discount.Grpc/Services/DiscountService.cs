using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(c => c.ProductName == request.ProductName) ?? new Coupon
        {
            ProductName = "No Discount", Amount = 0, Description = "No Discount Desc"
        };

        logger.LogInformation("> Discount is retrieved for Product Name: {productName}", coupon.ProductName);
        
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>() 
            ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request."));

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("> Discount is successfully created. Product Name: {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>() 
                     ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request."));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("> Discount is successfully updated. Product Name: {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FindAsync(request.ProductName)
                     ?? throw new RpcException(new Status(StatusCode.NotFound,
                         $"$Discount with Product Name={request.ProductName} is not found."));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("> Discount is successfully Deleted. Product Name: {productName}", coupon.ProductName);
        return new DeleteDiscountResponse()
        {
            Success = true
        };
    }
}