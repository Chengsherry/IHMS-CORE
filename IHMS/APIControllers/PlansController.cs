﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using IHMS.DTO;
using System.Numerics;

namespace IHMS.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IhmsContext _context;

        public PlansController(IhmsContext context)
        {
            _context = context;
        }


        // GET: api/Diet/{DietId}/deitdetail/")
        [Route("~/api/[controller]/Diet/{DietId:int}/deitdetail")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DietDetail>>> GetDietDetails(int DietId)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.DietDetails.Where(p => p.DietId == DietId).Select(p => new DietDetail
            {
                DietDetailId = p.DietDetailId,
                Decription =p.Decription,
                DietId = p.DietId,
                Fname =p.Fname,
                Type =p.Type,
                Calories =p.Calories,
                Img =p.Img,
            });
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return await res.ToListAsync();
            }

        }

        // GET: api/Plans/Sport/{SportId}/Sportdetail
        [Route("~/api/[controller]/Sport/{SportId:int}/Sportdetail")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportDetail>>> GetSportDetails(int SportId)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.SportDetails.Where(p => p.SportId == SportId).Select(p => new SportDetail
            {
                SportId = p.SportId,
                Description = p.Description,
                SportDetailId = p.SportDetailId,
                Sname = p.Sname,
                Image = p.Image,
                Sporttime = p.Sporttime,
                Frequency=p.Frequency,

            });
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return await res.ToListAsync();
            }

        }


        // GET: api/Plans/{PlanId}/Diet")
        [Route("~/api/[controller]/{PlanId:int}/Diet")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diet>>> GetDiets(int PlanId)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.Diets.Where(d => d.PlanId == PlanId).Select(d => new Diet
            {
               
                DietId = d.DietId,
                Registerdate =d.Registerdate,
                Date =d.Date
            });
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return await res.ToListAsync();
            }

        }

        // GET: api/Plans/Diet
        [Route("~/api/[controller]/{PlanId:int}/Sport")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sport>>> GetSports(int PlanId)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.Sports.Where(s => s.PlanId == PlanId).Select(s => new Sport
            {
                SportId = s.SportId,
                Registerdate=s.Registerdate,
                Date =s.Date

            });
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return await res.ToListAsync();
            }

        }




        // GET: api/Plans
        [Route("~/api/[controller]/member/{memberid:int}/{nums:int}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlansSideBarDTO>>> GetPlans(int memberid, int nums)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.Plans.Where(p => p.MemberId == memberid).OrderByDescending(p => p.RegisterDate).Take(nums).Select(p => new PlansSideBarDTO
            {
                PlanId = p.PlanId,
                Pname = p.Pname,
                RegisterDate = p.RegisterDate,
                EndDate = p.EndDate,
            });

            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return await res.ToListAsync();
            }

        }

        // GET: api/Plans
        [Route("~/api/[controller]/member/{memberid:int}/search/{search}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlansSideBarDTO>>> GetPlans(int memberid, string search)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.Plans.OrderByDescending(p => p.RegisterDate).Where(p => p.MemberId == memberid && p.Pname.Contains($"{search}")).Select(p => new PlansSideBarDTO
            {
                PlanId = p.PlanId,
                Pname = p.Pname,
                RegisterDate = p.RegisterDate,
                EndDate = p.EndDate,
            });

            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return await res.ToListAsync();
            }

        }


        // GET: api/Plans/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Plan>> GetPlan(int id)
        //{
        //  if (_context.Plans == null)
        //  {
        //      return NotFound();
        //  }
        //    var plan = await _context.Plans.FindAsync(id);

        //    if (plan == null)
        //    {
        //        return NotFound();
        //    }

        //    return plan;
        //}

        // PUT: api/Plans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlan(int id, Plan plan)
        {
            if (id != plan.PlanId)
            {
                return BadRequest();
            }

            _context.Entry(plan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Plans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plan>> PostPlan(CreatePlanDTO dto)
        {

            if (_context.Plans == null)
            {
                return Problem("Entity set 'IhmsContext.Plans' is null.");
            }
            Plan plan = new Plan
            {
                Pname = dto.pname,
                MemberId = dto.memberid,
                Weight = dto.weight,
                BodyPercentage = dto.Bmi,
                EndDate = dto.endDate,
                RegisterDate = DateTime.Now,
                Description = dto.description,
            };
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();

            return plan;
        }

        // DELETE: api/Plans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            DeletePlanDTO deleteitems = new DeletePlanDTO();

            if (_context.Plans == null)
            {
                return NotFound();
            }
            var plan = await _context.Plans.FindAsync(id);
            //移除plan的關聯資料
            deleteitems.diet = _context.Diets.Include("Plan").Where(d => d.PlanId == id);
            deleteitems.sport = _context.Sports.Include("Plan").Where(d => d.PlanId == id);
            deleteitems.water = _context.Water.Include("Plan").Where(d => d.PlanId == id);
            deleteMethod(deleteitems);

            if (plan == null)
            {
                return NotFound();
            }
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public void deleteMethod(DeletePlanDTO delete)
        {
            for (int i = 0; i < delete.deleteDataSet.Length; i++)
            {
                switch (delete.deleteDataSet[i])
                {
                    case "diet":
                        foreach (var diet in delete.diet)
                        {
                            var query = _context.DietDetails.Include("Diet").Where(d => d.DietId == diet.DietId);
                            foreach (var detail in query)
                            {
                                _context.DietDetails.Remove(detail);
                            }
                            _context.Diets.Remove(diet);
                        }

                        break;
                    case "sport":
                        foreach (var sport in delete.sport)
                        {
                            var query = _context.SportDetails.Include("Sport").Where(d => d.SportId == sport.SportId);
                            foreach (var detail in query)
                            {
                                _context.SportDetails.Remove(detail);
                            }
                            _context.Sports.Remove(sport);
                        }
                        break;
                    default:
                        foreach (var water in delete.water)
                        {
                            _context.Water.Remove(water);
                        }
                        break;
                }
            }
        }

        private bool PlanExists(int id)
        {
            return (_context.Plans?.Any(e => e.PlanId == id)).GetValueOrDefault();
        }
    }
}
