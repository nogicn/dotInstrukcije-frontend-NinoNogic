﻿using System.Security.Claims;
using DotgetPredavanje2.Data;
using DotgetPredavanje2.Models;
using DotgetPredavanje2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotgetPredavanje2.Controllers
{

    [ApiController]
    [Route("api")]
    public class SubjectController : Controller
    {
        private readonly AppContextExample context;
        private readonly IConfiguration configuration;
        
        public SubjectController(AppContextExample context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [Authorize]
        [HttpPost("subject")]
        public async Task<IActionResult> CreateSubject(SubjectCreateModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create a new Subject entity
            var subject = new Subject
            {
                Title = model.Title,
                Url = model.Url,
                Description = model.Description
            };

            // Add the new Subject to the database context
            context.Subject.Add(subject);

            // Save the changes to the database
            var result = await context.SaveChangesAsync();

            // Return a success response if the operation was successful, or an error response if it was not
            return result > 0
                ? Ok(new { success = true, message = "Subject created successfully." })
                : StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An error occurred while creating the subject." });
        }

        [Authorize]
        [HttpGet("subject/{url}")]
        public async Task<IActionResult> GetSubjectByUrl(string url)
        {
            // Query the database for a subject with the provided URL
            var subject = await context.Subject.FirstOrDefaultAsync(s => s.Url == url);

            // If a subject is found, return a success response with the subject data and associated professors
            if (subject != null)
            {
        
                // get all professors
                var professors = await context.Users.Where(u => u.Subjects != null).Select(
                    p => new
                    {
                        _id = p.ID,
                        p.Name,
                        p.Surname,
                        p.Email,
                        profilePictureUrl = p.ProfilePicture,
                        p.Subjects,
                        instructionsCount = p.InstructionsCount
                    }).ToListAsync();
                
                // check if professor has subject equal to subject url
                professors = professors != null ? professors.Where(p => p.Subjects.Contains(url)).ToList() : null;
                // print every subject that professor has
                
                return Ok(new
                {
                    success = true,
                    subject = new { subject.Title, subject.Url, subject.Description },
                    professors,
                    message = "Subject found."
                });
            }

            // If no subject is found, return an error response indicating that the subject was not found
            return NotFound(new { success = false, message = "Subject not found." });
        }

        
        [HttpGet("subjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            // Query the database to retrieve all subjects
            var subjects = await context.Subject.Select(
                s => new
                {
                    s.Title,
                    s.Url,
                    s.Description
                }).ToListAsync();


            // Return a success response with the list of subjects
            return Ok(new { success = true, subjects });
        }

        [Authorize]
        [HttpPost("instructions")]
        public async Task<IActionResult> ScheduleInstructionSession(InstructionSessionModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // get student id using email from token
            var studentEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var student = await context.Users.FirstOrDefaultAsync(u => u.Email == studentEmail);
            
            if (student == null) return NotFound(new { success = false, message = "Student not found." });

            // Create a new InstructionSession entity
            var instructionSession = new InstructionsDate
            {
                DateTime = model.Date,
                ProfessorId = model.ProfessorId,
                StudentId = student.ID,
                Status = "poslan zahtjev"
                
            };

            // Add the new InstructionSession to the database context
            context.InstructionsDate.Add(instructionSession);

            // Save the changes to the database
            var result = await context.SaveChangesAsync();

            // Return a success response if the operation was successful, or an error response if it was not
            return result > 0
                ? Ok(new { success = true, message = "Instruction session scheduled successfully." })
                : StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An error occurred while scheduling the instruction session." });
        }
        
        [Authorize]
        [HttpGet("instructions")]
        public async Task<IActionResult> GetInstructionSessions()
        {
            // Query the database to retrieve all instruction sessions
            var instructionSessions = await context.InstructionsDate.Select(
                i => new
                {
                    i.DateTime,
                    i.ProfessorId
                }).ToListAsync();

            // Return a success response with the list of instruction sessions
            return Ok(new { success = true, instructionSessions });
        }
        

    }
}