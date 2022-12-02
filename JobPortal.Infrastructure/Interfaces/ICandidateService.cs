using JobPortal.Domain.Models;
using JobPortal.Domain.Models.Search;

namespace JobPortal.Infrastructure.Interfaces
{
    public interface ICandidateService
    {
        /// <summary>
        /// get list of all candidates
        /// </summary>
        /// <returns></returns>
        List<Candidate> GetCandidateList(Search search);

        /// <summary>
        /// get candiate details by candiate id
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        Candidate GetCandidateDetailsById(int candidateId);

        /// <summary>
        ///  add edit candidate
        /// </summary>
        /// <param name="candidateModel"></param>
        /// <returns></returns>
        Response SaveCandidate(Candidate candidateModel);

        /// <summary>
        /// delete candiate
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        Response DeleteCandidate(int candidateId);
    }
}
