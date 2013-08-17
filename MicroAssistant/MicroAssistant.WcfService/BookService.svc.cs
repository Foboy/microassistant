using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MicroAssistant.Common;
using MicroAssistant.Cache;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistant.DataAccess;

namespace MicroAssistant.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“BookService”。
    public class BookService : IBookService
    {
        public void Close()
        { }
        #region IBookService 成员

        /// <summary>
        /// 绑定成长记录图片
        /// </summary>
        /// <param name="bookid">成长记录ID</param>
        /// <param name="picid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public RespResult BindBookPic(int bookid, int picid,string description,DateTime createtime, string token)
        {
            RespResult result = new RespResult();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {

                    ResPic pic = ResPicAccessor.Instance.Get(picid);
                    pic.ObjId = bookid;
                    pic.ObjType = PicType.Book;
                    pic.PicDescription = description;
                    pic.CreateTime = createtime;

                    ResPicAccessor.Instance.Update(pic);

                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        public AdvancedResult<int> AddBook(string bookname, BookSize booksize,BookCoverType bookcover, int booktype, string introduction, string token)
        {
            AdvancedResult<int> result = new AdvancedResult<int>();
            try
            {
                if (string.IsNullOrEmpty(bookname))
                {
                    result.Error = AppError.ERROR_BOOK_NOT_NULL;
                    return result;
                }
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));

                //int userid = 5;
                    ProBook book = new ProBook();

                    book.BookName = bookname.Trim();
                    book.BookSize = booksize;
                    book.BookType = booktype;
                    book.AdUserId = userid;
                    book.BookCover = bookcover;
                    book.Introduction = string.IsNullOrEmpty(introduction) ? string.Empty : introduction.Trim();

                    result.Data = ProBookAccessor.Instance.Insert(book);

                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        public RespResult EditBook(int bookid, string bookname, BookSize booksize, BookCoverType bookcover, int booktype, string introduction, string token)
        {
            RespResult result = new RespResult();
            try
            {
                if (string.IsNullOrEmpty(bookname))
                {
                    result.Error = AppError.ERROR_BOOK_NOT_NULL;
                    return result;
                }

                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {

                    ProBook book = new ProBook();
                    book.BookId = bookid;
                    book.BookName = bookname.Trim();
                    book.BookSize = booksize;
                    book.BookType = booktype;
                    book.BookCover = bookcover;
                    book.Introduction = string.IsNullOrEmpty(introduction) ? string.Empty : introduction.Trim();
                    book.State = StateType.Active;

                    ProBookAccessor.Instance.Update(book);

                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        public AdvancedResult<ProBook> GetBook(int bookid)
        {
            AdvancedResult<ProBook> result = new AdvancedResult<ProBook>();
            try
            {
                    result.Data = ProBookAccessor.Instance.Get(bookid,StateType.Active);

                    result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        /// <summary>
        /// 获取用户书架
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public AdvancedResult<List<ProBook>> SearchBooks(string token,int pageIndex,int pageSize)
        {
            AdvancedResult<List<ProBook>> result = new AdvancedResult<List<ProBook>>();
            try
            {
                List<ProBook> booklist = new List<ProBook>();
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    booklist = ProBookAccessor.Instance.Search(userid, StateType.Active, pageIndex, pageSize).Items;
                    result.Data = booklist;
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        /// <summary>
        /// 查询系统用户默认相册
        /// </summary>
        /// <returns></returns>
        public AdvancedResult<List<ProBook>> SearchBooks()
        {
            AdvancedResult<List<ProBook>> result = new AdvancedResult<List<ProBook>>();
            try
            {
                List<ProBook> booklist = new List<ProBook>();
                    booklist = ProBookAccessor.Instance.Search(5, StateType.Active, 0, 3).Items;
                    result.Data = booklist;
                    result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        public RespResult DeleteBook(int bookid, string token)
        {
            RespResult result = new RespResult();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    if (ProBookAccessor.Instance.Delete(bookid))
                    {
                        result.Error = AppError.ERROR_SUCCESS;
                    }
                    else
                    {
                        result.Error = AppError.ERROR_FAILED;
                    }
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        #endregion
    }
}
