using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using System.Web.Configuration;

namespace EntityPractice
{
    public partial class Entity : System.Web.UI.Page
    {        
        private IntelisisTmpEntities db = new IntelisisTmpEntities();
        
        protected void Page_Load(object sender, EventArgs e)
        {                        
            if(!IsPostBack)
                //fillCombo();

            showData();
        }

        protected void fillCombo()
        {
            var query = (from l in db.TblEntity
                         select l.Genero).Distinct();
            cmbFiltro.Items.Clear();
            cmbFiltro.Items.Add("TODO");

            foreach (var b in query)
            {
                cmbFiltro.Items.Add(b);
            }            
        }

        protected void Limpiar()
        {
            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtGenero.Text = "";
            txtTitulo.Focus();
        }

        protected void showData()
        {
            try
            {
                if (cmbFiltro.Text != "TODO")
                {
                    var query = from l in db.TblEntity
                                where l.Genero == cmbFiltro.Text
                                select l;
                    gvEntity.DataSource = query.ToList();
                    gvEntity.DataBind();
                }
                else
                {
                    var query = from l in db.TblEntity
                                select l;
                    gvEntity.DataSource = query.ToList();
                    gvEntity.DataBind();
                }
            }
            catch { }
        }

        protected void addBook()
        {
            if (!string.IsNullOrEmpty(txtTitulo.Text) && !string.IsNullOrEmpty(txtAutor.Text) && !string.IsNullOrEmpty(txtGenero.Text) && btnAgregar.Text == "Agregar")
            {
                TblEntity books = new TblEntity()
                {
                    Titulo = txtTitulo.Text.ToUpper(),
                    Autor = txtAutor.Text.ToUpper(),
                    Genero = txtGenero.Text.ToUpper()
                };

                db.TblEntity.Add(books);
                db.SaveChanges();
            }
        }

        protected void updateBook(int code)
        {
            if (!string.IsNullOrEmpty(txtTitulo.Text) && !string.IsNullOrEmpty(txtAutor.Text) && !string.IsNullOrEmpty(txtGenero.Text) && btnAgregar.Text == "Actualizar")
            {
                TblEntity books = (from u in db.TblEntity
                                   where u.Id == code
                                   select u).FirstOrDefault();
                books.Titulo = txtTitulo.Text.ToUpper();
                books.Autor = txtAutor.Text.ToUpper();
                books.Genero = txtGenero.Text.ToUpper();

                db.SaveChanges();
            }
        }

        protected void deleteBook(int code)
        {
            TblEntity books = (from d in db.TblEntity
                               where d.Id == code
                               select d).FirstOrDefault();
            db.TblEntity.Remove(books);
            db.SaveChanges();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if(btnAgregar.Text == "Agregar")
            {
                addBook();
                fillCombo();
            }
            if (btnAgregar.Text == "Actualizar")
            {
                updateBook(Convert.ToInt32(gvEntity.SelectedRow.Cells[0].Text));
                fillCombo();
                try { cmbFiltro.SelectedValue = Server.HtmlEncode(Request.Cookies["UltimoCombo"].Value); }
                catch { cmbFiltro.SelectedValue = "TODO"; }
            }
            
            Limpiar();
            showData();

            gvEntity.SelectedIndex = -1;
            btnAgregar.Text = "Agregar";
            btnEliminar.Enabled = false;
        }
        
        protected void gvEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvEntity.Rows)
            {
                if(row.RowIndex == gvEntity.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#538C84");    //#82BF7B   #1C5E55
                    row.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Seleccione Fila";
                    txtTitulo.Text = HttpUtility.HtmlDecode(gvEntity.SelectedRow.Cells[1].Text);
                    txtAutor.Text = HttpUtility.HtmlDecode(gvEntity.SelectedRow.Cells[2].Text);
                    txtGenero.Text = HttpUtility.HtmlDecode(gvEntity.SelectedRow.Cells[3].Text);
                    btnAgregar.Text = "Actualizar";
                    btnEliminar.Enabled = true;
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ForeColor = ColorTranslator.FromHtml("#000000");
                }
            }
        }

        protected void gvEntity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvEntity, "Select$" + e.Row.RowIndex.ToString());
        }

        protected void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            showData();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            gvEntity.SelectedIndex = -1;
            btnAgregar.Text = "Agregar";
            btnEliminar.Enabled = false;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            deleteBook(Convert.ToInt32(gvEntity.SelectedRow.Cells[0].Text));

            Limpiar();
            showData();
            fillCombo();

            gvEntity.SelectedIndex = -1;
            btnAgregar.Text = "Agregar";
            btnEliminar.Enabled = false;
        }

        #region Ajax para asignar cookie
        [System.Web.Services.WebMethod]
        public static bool Asignar(string filtro)
        {
            try
            {
                HttpCookie aCookie = new HttpCookie("UltimoCombo");
                aCookie.Value = filtro;
                HttpContext.Current.Response.Cookies.Add(aCookie);
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }       
        #endregion
    }
}