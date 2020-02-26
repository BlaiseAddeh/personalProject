using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using QUIZ_SYSTEM.Models;
using System.IO;

namespace QUIZ_SYSTEM.Controllers
{
	public class HomeController : Controller
	{
		DBQuizEntities db = new DBQuizEntities();

		[HttpGet]		
		public ActionResult sregister()
		{
			return View();
		}

		[HttpPost]
		public ActionResult sregister( TBL_STUDENT svw, HttpPostedFileBase imgfile )
		{
			try
			{
				TBL_STUDENT s = new TBL_STUDENT();
				s.S_NAME = svw.S_NAME;
				s.S_PASSWORD = svw.S_PASSWORD;
				s.S_IMAGE = uploadImage(imgfile);
				db.TBL_STUDENT.Add(s);
				db.SaveChanges();
				return RedirectToAction("slogin");
			}
			catch(Exception)
			{
				ViewBag.msg = "Data could not be inserted...";
			}

			return View();
		}

		public string uploadImage(HttpPostedFileBase imgfile)
		{
			Random r = new Random();
			string path = "-1";
			int random = r.Next();
			try
			{
				if (imgfile !=null && imgfile.ContentLength > 0)
				{
					string extension = Path.GetExtension(imgfile.FileName);
					if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") ||extension.ToLower().Equals(".png"))
					{
						try
						{
							path = Path.Combine(Server.MapPath("~/Content/images"), random + Path.GetFileName(imgfile.FileName));
							imgfile.SaveAs(path);
							path = "~/Content/images" + random + Path.GetFileName(imgfile.FileName);
						}
						catch (Exception)
						{
							path ="-1";
						}						
					}
					else
					{
						Response.Write("<script>alert('Seuls les format jpg, jpeg ou png sont acceptés....')</script>");
					}
				}
				else
				{
					Response.Write("<script>alert('Selectionnez une photo svp')</script>");
				}
			}
			catch (Exception)
			{
				throw;
			}
			return path;
		}


		public ActionResult Logout()
		{
			Session.Abandon();
			Session.RemoveAll();
			return RedirectToAction("Index");
		}

		public ActionResult Index()
		{
			if (Session["ad_id"] !=null)
			{
				return RedirectToAction("Dashboard");
			}
			return View();
		}

		[HttpGet]
		public ActionResult tlogin()
		{
			return View();
		}

		[HttpPost]
		public ActionResult tlogin(TBL_ADMIN a)
		{
			TBL_ADMIN ad = db.TBL_ADMIN.Where(x => x.AD_NAME == a.AD_NAME && x.AD_PASSWORD == a.AD_PASSWORD).SingleOrDefault();
			if (ad !=null)
			{
				Session["ad_id"] = ad.AD_ID;
				return RedirectToAction("Dashboard");
			}
			else
			{
				ViewBag.msg = "Invalid username or password";
			}
			return View();
		}

		public ActionResult slogin()
		{
			return View();
		}

		[HttpPost]
		public ActionResult slogin(TBL_STUDENT s)
		{
			TBL_STUDENT std = db.TBL_STUDENT.Where(x => x.S_NAME == s.S_NAME && x.S_PASSWORD == s.S_PASSWORD).FirstOrDefault();
			if (std == null)
			{
				ViewBag.msg = "Invalid Email or Password";
			}
			else
			{				

				Session["std_id"] = std.S_ID;

				HttpCookie cookie = new HttpCookie("UserDetails");
				cookie["Name"] = std.S_NAME;
				if (std.S_IMAGE.Equals("-1"))
				{
					cookie["image"] = "~/Content/images/Unknown-person.jpeg";
				}
				else
				{
					cookie["image"] = std.S_IMAGE;
				}
				// Cookie will be persited for 30 days
				cookie.Expires = DateTime.Now.AddDays(30);
				//Add the cookie to the client machine
				Response.Cookies.Add(cookie);

				return RedirectToAction("StudentExam");
			}

			return View();
		}

		public ActionResult viewAllquestions(int? id)
		{
			if (Session["ad_id"] == null)
			{
				return RedirectToAction("tlogin");
			}

			if (id==null)
			{
				return RedirectToAction("Dashboard");
			}
			return View(db.TBL_QUESTIONS.Where(x => x.Cat_Id == id).ToList());
		}

		public ActionResult records(int? page)
		{
			if (Session["ad_id"] == null)
			{
				return RedirectToAction("Index");
			}

			return View();			
		}

		public ActionResult StudentExam()
		{
			if (Session["std_id"] == null)
			{
				return RedirectToAction("slogin");
			}
			return View();
		}

		[HttpPost]
		public ActionResult StudentExam(string room)
		{
			List<TBL_Category> list = db.TBL_Category.ToList();
			TempData["score"] = 0;

			foreach (var item in list)
			{
				if (item.Cat_encryptedstring == room)
				{

					List<TBL_QUESTIONS> li = db.TBL_QUESTIONS.Where(x => x.Cat_Id == item.Cat_Id).ToList();

					Queue<TBL_QUESTIONS> queue = new Queue<TBL_QUESTIONS>();
					foreach (TBL_QUESTIONS a in li)
					{
						queue.Enqueue(a);
					}
					TempData["examid"] = item.Cat_Id;
					TempData["questions"] = queue;
					TempData["score"] = 0;

					TempData.Keep();
					return RedirectToAction("QuizStart");
				}
				else
				{
					ViewBag.error = "No Room Found...";
				}
			}
			
			return View();
		}

		public ActionResult QuizStart()
		{
			if (Session["std_id"] == null)
			{
				return RedirectToAction("slogin");
			}

			TBL_QUESTIONS q = null;

			if (TempData["questions"] !=null)
			{
				Queue<TBL_QUESTIONS> qlist =(Queue<TBL_QUESTIONS>)TempData["questions"];
				if (qlist.Count > 0)
				{
					q = qlist.Peek();
					qlist.Dequeue();
					TempData["questions"] = qlist;
					TempData.Keep();
				}
				else
				{
					return RedirectToAction("EndExam");
				}
			}
			else
			{
				return RedirectToAction("StudentExam");
			}

			return View(q);
		}

		[HttpPost]
		public ActionResult QuizStart(TBL_QUESTIONS q)
		{
			string correctans = null;

			if (q.OPA !=null)
			{
				correctans = "A";
			}
			else if (q.OPB != null)
			{
				correctans = "B";
			}
			else if (q.OPC != null)
			{
				correctans = "C";
			}
			else if (q.OPD != null)
			{
				correctans = "D";
			}

			if (correctans.Equals(q.COP))
			{
				TempData["score"] = Convert.ToInt32(TempData["score"]) + 1;
			}

			TempData.Keep();

			return RedirectToAction("QuizStart");
		}

		public ActionResult EndExam()
		{
			if (Session["std_id"] == null)
			{
				return RedirectToAction("slogin");
			}

			return View();
		}


		public ActionResult Dashboard()
		{
			if (Session["ad_id"] == null)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet]
		public ActionResult Addcategory()
		{
			if (Session["ad_id"] == null)
			{
				return RedirectToAction("Index");
			}

			int adid = Convert.ToInt32(Session["ad_id"].ToString());
			List<TBL_Category> li = db.TBL_Category
				                .Where(x=>x.AD_ID == adid)
								.OrderByDescending(x => x.Cat_Id).ToList();
			ViewData["list"] = li;

			return View();
		}

		[HttpPost]
		public ActionResult Addcategory(TBL_Category cat)
		{
			if (Session["ad_id"] == null)
			{
				return RedirectToAction("Index");
			}

			List<TBL_Category> li = db.TBL_Category.OrderByDescending(x => x.Cat_Id).ToList();
			ViewData["list"] = li;

			Random r = new Random();

			TBL_Category c = new TBL_Category();
			c.Cat_Name = cat.Cat_Name;
			c.Cat_encryptedstring = crypto.Encrypt(cat.Cat_Name.Trim()+ r.Next().ToString(), true);
			c.AD_ID = Convert.ToInt32(Session["ad_id"].ToString());
			db.TBL_Category.Add(c);
			db.SaveChanges();

			return RedirectToAction("Addcategory");
		}

		[HttpGet]
		public ActionResult Addquestion()
		{
			if (Session["ad_id"] == null)
			{
				return RedirectToAction("Index");
			}
			int sid = Convert.ToInt32(Session["ad_id"]);
			List<TBL_Category> li = db.TBL_Category.Where(x => x.AD_ID == sid).ToList();
			ViewBag.list = new SelectList(li, "Cat_Id", "Cat_Name");
			return View();
		}

		[HttpPost]
		public ActionResult Addquestion(TBL_QUESTIONS q)
		{
			if (Session["ad_id"] == null)
			{
				return RedirectToAction("Index");
			}
			int sid = Convert.ToInt32(Session["ad_id"]);
			List<TBL_Category> li = db.TBL_Category.Where(x => x.AD_ID == sid).ToList();
			ViewBag.list = new SelectList(li, "Cat_Id", "Cat_Name");

			TBL_QUESTIONS QA = new TBL_QUESTIONS();
			QA.Q_TEXT = q.Q_TEXT;
			QA.OPA = q.OPA;
			QA.OPB = q.OPB;
			QA.OPC = q.OPC;
			QA.OPD = q.OPD;
			QA.COP = q.COP;
			QA.Cat_Id = q.Cat_Id;

			db.TBL_QUESTIONS.Add(QA);
			db.SaveChanges();

			TempData["msg"] = "Question added successfully...";
			TempData.Keep();

			return RedirectToAction("Addquestion");
		}



		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}