using JobPortal.Domain;
using JobPortal.Domain.Models;
using JobPortal.Domain.Models.Search;
using JobPortal.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace JobPortal.Infrastructure.Services
{
    public class CandidateService : ICandidateService
    {
        private DatabaseContext _context;

        public CandidateService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get list of all candidates
        /// </summary>
        /// <returns></returns>
        public List<Candidate> GetCandidateList(Search search)
        {
            List<Candidate> candidateList;
            try
            {
                if (!string.IsNullOrEmpty(search.SearchText) && search.SearchText != "null")
                {

                    candidateList = _context.Candidate.Where(a => a.Email.Contains(search.SearchText) || a.Name.Contains(search.SearchText)
                                                            || a.PlaceOfBirth.Contains(search.SearchText)).ToList();
                }
                else
                {
                    candidateList = _context.Candidate.ToList();
                }

                if (candidateList != null && candidateList.Count > 0)
                {
                    candidateList = PagedList<Candidate>.ToPagedList(candidateList, search.PageNumber, search.PageSize);

                    if (search.SortOrder >= 0 && search.SortBy != null)
                    {
                        candidateList = SortList(candidateList, search.SortOrder, search.SortBy);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return candidateList;
        }

        /// <summary>
        /// get candiate details by candiate id
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public Candidate GetCandidateDetailsById(int candidateId)
        {
            Candidate candidate;
            try
            {
                candidate = _context.Find<Candidate>(candidateId);
            }
            catch (Exception)
            {
                throw;
            }
            return candidate;
        }

        /// <summary>
        ///  add edit candidate
        /// </summary>
        /// <param name="candidateModel"></param>
        /// <returns></returns>
        public Response SaveCandidate(Candidate candidateModel)
        {
            Response model = new Response();
            try
            {
                Candidate _temp = GetCandidateDetailsById(candidateModel.Id);
                if (_temp != null)
                {
                    _temp.Name = candidateModel.Name;
                    _temp.Email = candidateModel.Email;
                    _temp.DateOfBirth = candidateModel.DateOfBirth;
                    _temp.PlaceOfBirth = candidateModel.PlaceOfBirth;
                    _temp.JobId = candidateModel.JobId;
                    _context.Update<Candidate>(_temp);
                    model.Messsage = "Candidate Update Successfully";
                }
                else
                {
                    var existingCandidate = _context.Candidate.FirstOrDefault(a => a.Email.Trim().ToLower() == candidateModel.Email.Trim().ToLower());
                    if (existingCandidate == null)
                    {
                        _context.Add<Candidate>(candidateModel);
                        _context.SaveChanges();
                        model.Success = true;
                        model.Messsage = "Candidate Inserted Successfully";
                    }
                    else
                    {
                        model.Success = false;
                        model.Messsage = "Candidate with same email already exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                model.Success = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        /// <summary>
        /// delete candidate
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public Response DeleteCandidate(int candidateId)
        {
            Response model = new Response();
            try
            {
                Candidate _temp = GetCandidateDetailsById(candidateId);
                if (_temp != null)
                {
                    _context.Remove<Candidate>(_temp);
                    _context.SaveChanges();
                    model.Success = true;
                    model.Messsage = "Candidate Deleted Successfully";
                }
                else
                {
                    model.Success = false;
                    model.Messsage = "Candidate Not Found";
                }
            }
            catch (Exception ex)
            {
                model.Success = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        private List<Candidate> SortList(List<Candidate> candidateList, int sortOrder, string sortBy)
        {
            switch (sortBy)
            {
                case "Name":
                    if (sortOrder == 0)
                    {
                        candidateList = candidateList.OrderBy(a => a.Name).ToList();
                    }
                    else
                    {
                        candidateList = candidateList.OrderByDescending(a => a.Name).ToList();
                    }
                    break;
                case "Email":
                    if (sortOrder == 0)
                    {
                        candidateList = candidateList.OrderBy(a => a.Email).ToList();
                    }
                    else
                    {
                        candidateList = candidateList.OrderByDescending(a => a.Email).ToList();
                    }
                    break;
                case "PlaceOfBirth":
                    if (sortOrder == 0)
                    {
                        candidateList = candidateList.OrderBy(a => a.PlaceOfBirth).ToList();
                    }
                    else
                    {
                        candidateList = candidateList.OrderByDescending(a => a.PlaceOfBirth).ToList();
                    }
                    break;
                case "JobId":
                    if (sortOrder == 0)
                    {
                        candidateList = candidateList.OrderBy(a => a.JobId).ToList();
                    }
                    else
                    {
                        candidateList = candidateList.OrderByDescending(a => a.JobId).ToList();
                    }
                    break;
            }

            return candidateList;
        }
    }
}
