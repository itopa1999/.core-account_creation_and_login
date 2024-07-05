using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using onboarding.Models;
using onboarding.Dtos.Account;



namespace onboarding.Controllers
{   
    [Microsoft.AspNetCore.Mvc.Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager){
            _userManager = userManager;
        }

      [HttpPost("register")]
      public async Task<IActionResult> Register([FromBody] RegisterDto registerDto){
        try{
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = new AppUser{
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var CreatedUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if(CreatedUser.Succeeded){
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded){
                    return Ok("User created");
                }else{
                    return StatusCode(500, roleResult.Errors);
                }
            }else{
                return StatusCode(500, CreatedUser.Errors);
            }
        } catch (Exception e){
            return StatusCode(500, e);
        }
      }



    }
}