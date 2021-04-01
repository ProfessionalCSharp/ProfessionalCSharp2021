using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BookChaptersController : ControllerBase
    {
        private readonly IBookChapterService _chapterService;
        static readonly string[] readScopesRequired = { "Books.Read" };
        static readonly string[] writeScopesRequired = { "Books.Write" };

        public BookChaptersController(IBookChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        // GET api/bookchapters/guid
        [HttpGet]
        public Task<IEnumerable<BookChapter>> GetBookChapters()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(readScopesRequired);

            return _chapterService.GetAllAsync();
        }

        // GET api/bookchapters/guid
        [HttpGet("{id}", Name = nameof(GetBookChapterById))]
        public async Task<ActionResult<IEnumerable<BookChapter>>> GetBookChapterById(Guid id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(readScopesRequired);

            BookChapter? chapter = await _chapterService.FindAsync(id);
            if (chapter is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(chapter);
            }
        }

        // POST api/bookchapters
        [HttpPost]
        public async Task<ActionResult> PostBookChapter(BookChapter chapter)
        {
            if (chapter is null)
            {
                return BadRequest();
            }
            await _chapterService.AddAsync(chapter);
            return CreatedAtRoute(nameof(GetBookChapterById), new { id = chapter.Id }, chapter);
        }

        // PUT api/bookchapters/guid
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBookChapter(Guid id, BookChapter chapter)
        {
            if (chapter is null || id != chapter.Id)
            {
                return BadRequest();
            }
            var existingChapter = await _chapterService.FindAsync(id);

            var c = await _chapterService.UpdateAsync(chapter);
            if (c is null)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }

        // DELETE api/bookchapters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existingChapter = await _chapterService.RemoveAsync(id);
            if (existingChapter is null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}
