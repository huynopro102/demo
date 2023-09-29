using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly QlspContext _context;
        public ProductController(QlspContext context)
        {
            _context = context;
        }
        [HttpGet("/api/category")]
        public IActionResult GetAllCatagory()
        {

            var catagory = _context.Categories.ToList();
            if (catagory != null)
            {
                return Ok(catagory);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var product1 = _context.Products.ToList();
            if (product1 != null)
            {
                return Ok(product1);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public IActionResult Get(string id) {

            var product1 = _context.Products.FirstOrDefault(p => p.ProductId.Trim() == id.Trim());
            if (product1 != null)
            {
                if (product1.ProductId.Trim() == id.Trim())
                {
                    return Ok(product1);
                }
                else
                {
                return BadRequest();
                }
            }
            else
            {
                    return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            if (product != null)
            {
                try
                {
                    if (product.ProductId == null || product.ProductName == null || product.Price == null || product.Category == null)
                    {
                        return BadRequest(product);
                    }
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    return Ok(product);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(Product updatedProduct)
        {
            // Kiểm tra xem người dùng đã nhập body chưa , lỗi định dạng
            if (updatedProduct == null)
            {
                return BadRequest("Vui lòng cung cấp dữ liệu trong phần body của yêu cầu.");
            }

            try
            {
                var sanpham = _context.Products.FirstOrDefault(p => p.ProductId.Trim() == updatedProduct.ProductId.Trim());

                if (sanpham != null)
                {
                    // Cập nhật thông tin sản phẩm từ updatedProduct
                    sanpham.ProductName = updatedProduct.ProductName?.Trim();
                    sanpham.Price = updatedProduct.Price;
                    sanpham.Category = updatedProduct.Category;
                    sanpham.CategoryNavigation = updatedProduct.CategoryNavigation;

                    _context.SaveChanges();

                    return Ok(new
                    {
                        sanpham
                    });
                }
                else
                {
                    return NotFound(); // Không tìm thấy sản phẩm để cập nhật
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //  lỗi định dạng 
            }
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
            var deleteUser = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (deleteUser != null)
            {
                _context.Products.Remove(deleteUser);
                _context.SaveChanges();
                return Ok(new
                {
                    trang_thai = true,
                    deleteUser
                });
            }
            else
            {
                return NotFound();
            }

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }





    }

}
