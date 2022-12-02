using JobPortal.Domain.Models;
using JobPortal.Domain.Models.Search;
using JobPortal.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Core.Controllers
{
    public class CandidateController : Controller
    {
        ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        /// <summary>
        /// get all candidates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("candidate")]
        public IActionResult GetAllCandidates([FromQuery] Search search)
        {
            try
            {
               var candidates = _candidateService.GetCandidateList(search);
                if (candidates == null)
                {
                    return NotFound();
                }
                return Ok(candidates);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// get candidate details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("candidate/id")]
        public IActionResult GetCandidateById(int id)
        {
            try
            {
                var candidate = _candidateService.GetCandidateDetailsById(id);
                if (candidate == null) return NotFound();
                return Ok(candidate);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// save candidate
        /// </summary>
        /// <param name="candidateModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("candidate")]
        public IActionResult SaveCandidate([FromBody] Candidate candidateModel)
        {
            try
            {
                var model = _candidateService.SaveCandidate(candidateModel);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// delete candidate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("candidate/{id}")]
        public IActionResult DeleteCandidate(int id)
        {
            try
            {
                var model = _candidateService.DeleteCandidate(id);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
