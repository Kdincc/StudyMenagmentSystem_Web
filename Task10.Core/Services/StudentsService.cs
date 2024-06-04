using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Core.Services
{
    public sealed class StudentsService(IStudentsRepository studentsRepository, IGroupsRepository groupsRepository) : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository = studentsRepository;
        private readonly IGroupsRepository _groupsRepository = groupsRepository;

        public async Task CreateStudentAsync(string studentName, string lastName, int groupId)
        {
            var student = new Student { Name = studentName, LastName = lastName, GroupId = groupId };

            await _studentsRepository.AddAsync(student);
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            await _studentsRepository.DeleteAsync(studentId);
        }

        public async Task EditStudentAsync(string name, string lastName, int studentId, int groupId)
        {
            Student student = await _studentsRepository.GetByIdAsync(studentId);

            student.Name = name;
            student.LastName = lastName;
            student.GroupId = groupId;

            await _studentsRepository.UpdateAsync(studentId);
        }

        public async Task<StudentEditDto> GetEditStudentDtoAsync(int id)
        {
            Student student = await _studentsRepository.GetByIdAsync(id);
            var studentDto = new StudentDto 
            { 
                Id = student.Id,
                Name = student.Name,
                LastName = student.LastName,
                GroupId = student.GroupId,
            };

            IEnumerable<GroupDto> groups = await GetGroupsAsync();

            return new StudentEditDto { Student = studentDto, Groups = groups };
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync()
        {
            IEnumerable<Group> groups = await _groupsRepository.GetAllAsync();

            IEnumerable<GroupDto> groupDtos = groups.Select(group => new GroupDto { Name = group.Name, Id = group.Id });

            return groupDtos;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsWithGroupsNameAsync()
        {
            IEnumerable<Student> students =  await _studentsRepository.GetStudentWithGroupsAsync();

            IEnumerable<StudentDto> studentDtos = students.Select(
                students => new StudentDto 
                {  
                    Name = students.Name, 
                    LastName = students.LastName,
                    Id = students.Id, 
                    GroupName = students.Group.Name,
                    GroupId = students.GroupId 
                });

            return studentDtos;
        }
    }
}
