using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookChaptersController : ControllerBase
    {
        private readonly IBookChapterService _chapterService;

        public BookChaptersController(IBookChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        // GET api/bookchapters/guid
        [HttpGet]
        public Task<IEnumerable<BookChapter>> GetBookChapters() => 
            _chapterService.GetAllAsync();

        // GET api/bookchapters/guid
        [HttpGet("{id}", Name = nameof(GetBookChapterById))]
        public async Task<ActionResult<IEnumerable<BookChapter>>> GetBookChapterById(Guid id)
        {
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
