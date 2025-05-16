using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL;

namespace BLL.services
{
    public class HeadingService
    {
        IMapper mapper;
        public HeadingService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public bool AddHeading(UnitOfWork unitOfWork, HeadingDto headingDto)
        {
            if (FindHeading(unitOfWork, headingDto.Name) != null)
                throw new ValidationException("Така рубрика вже існує");

            var heading = mapper.Map<Heading>(headingDto);
            unitOfWork.GetRepository<Heading>().Add(heading);
            unitOfWork.Save();
            return true;
        }

        public HeadingDto FindHeading(UnitOfWork unitOfWork, string name)
        {
            return mapper.Map<HeadingDto>(unitOfWork.GetRepository<Heading>().Find(h => h.Name == name));
        }

        public List<HeadingDto> FindAllHeadings(UnitOfWork unitOfWork)
        {
            return mapper.Map<List<HeadingDto>>(unitOfWork.GetRepository<Heading>().GetAll());
        }

    }
}
