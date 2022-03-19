<%@ Page Title="View Flashcards" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewFlashcards.aspx.cs" Inherits="Flashminder.ViewFlashcards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-2.2.3.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.flashcard').click(function (e) {
                var target = $(e.currentTarget).context;
                if (target.classList.contains('flashcard')) {
                    if (target.children[0].innerText) {
                        window.location.href = 'EditFlashcard.aspx?FlashcardID=' + target.children[0].innerText;// this should be the flashcard id element
                    }
                }
            });
            $('#delete_btn').click(function (e) {
                if (confirm('Are you sure you want to delete this item?')) {
                    $.ajax({
                        url: '/api/flashcard/' + e.currentTarget.parentElement.parentElement.firstElementChild.innerText,
						type: 'DELETE',
                        success: function (response) {
							window.location.href = 'ViewFlashcards.aspx';
						}
                    });
					window.location.href = 'ViewFlashcards.aspx';
                }
            });

        });
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h2>Flashcards</h2>
                <div class="form-control" style="margin-bottom: 40px;">
                    <asp:Label ID="category_lbl" runat="server">Category: </asp:Label>
                    <asp:DropDownList ID="category_dropdownlist" runat="server" DataTextField="CategoryName" DataValueField="Id" AppendDataBoundItems="true" OnSelectedIndexChanged="SwitchCategory" AutoPostBack="true">
                        <asp:ListItem Selected="True" Text="All" Value="All"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-5">
                    <div class="label label-info">Front</div></div>
                        <div class="label label-info">Back</div>
                <asp:DataList ID="flashcards_datalist" runat="server">
                    <ItemTemplate>
                        <div class="flashcard" runat="server">
                            <div id="id" class="flashcard-id" hidden="hidden"><%# Eval("Id") %></div>

                            <div class="col-md-4" style="margin-right: 20px; margin-top:20px;">
                                <asp:Image CssClass="thumbnail" runat="server" Height="100" Width="200" ID="FrontImage" Visible='<%# Eval("FrontImage") == null ? false : System.IO.File.Exists(Server.MapPath(("Images/"+ Eval("FrontImage").ToString()))) %>' ImageUrl='<%# ("Images/"+ Eval("FrontImage")?.ToString())%>' />
                            
                                <%# Eval("FrontText")%>
                            </div>
                            <div class="col-md-4" style="margin-right: 20px; margin-top:20px;">
                                <asp:Image CssClass="thumbnail" runat="server" Height="100" Width="200" ID="BackImage" Visible='<%# Eval("BackImage") == null ? false : System.IO.File.Exists(Server.MapPath(("Images/"+ Eval("BackImage").ToString()))) %>' ImageUrl='<%# ("Images/"+ Eval("BackImage")?.ToString())%>' />
                               
                                <%# Eval("BackText")%>
                            </div>
                            <div class="col-md-2">
                                <button id="delete_btn" class="btn btn-danger" style="margin-top: 20px;"><i class="fa fa-minus" aria-hidden="true" > Delete</i></button>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="col-md-8" style="margin-top:40px;">
                    <a href="ViewDashboard.aspx" class="btn btn-warning"><i class="fa fa-arrow-left" aria-hidden="true"> Back to Dashboard</i></a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
