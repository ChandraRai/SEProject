<%@ Page Language="C#" Title="Create Flashcard" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CreateFlashcard.aspx.cs" Inherits="Flashminder.CreateFlashcard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="Scripts/jquery-2.2.3.js"></script>
    <script type="text/javascript">

        function OpenPopup() {

            var newWindow = window.open("CreateCategory.aspx", "List", "toolbar=no, location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes, width=500,height=300,left=430,top=100");
            newWindow.onbeforeunload = function () {
                window.location.href = window.location.href;
            }

            return false;
        }

        function ValidateForm() {
            var ret = true;
			if (!$("#MainContent_front_txtbx").val() && !$("#MainContent_front_upload").val()) {
                ShowMessage("Front of the card needs text or an image", "warning");
                ret = false;
            }
			if ((!$("#MainContent_back_txtbx").val() && !$("#MainContent_back_upload").val())) {
                ShowMessage("Back of the card needs text or an image", "warning");
                ret = false;
            }

			if ($("#MainContent_front_txtbx").val() && $("#MainContent_front_txtbx").val().length > 500) {
                ShowMessage("Text can only be 500 characters", "warning");
                ret = false;
            }
			if ($("#MainContent_back_txtbx").val() && $("#MainContent_back_txtbx").val().length > 500) {
                ShowMessage("Text can only be 500 characters", "warning");
                ret = false;
            }

            return ret;
        }

        function ShowMessage(msg, type) {
            $("#MessagePanel").html('<div class="message_pnl"><a href="#" class ="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + msg + '</span></div>')
            $(".message_pnl").addClass("alert alert-" + type + " alert-dismissable");
            $(".message_pnl").attr("role", "alert");
            $(".message_pnl").show();
        }


        //https://stackoverflow.com/questions/28214458/how-to-preview-uploaded-image-in-file-upload-control-before-save-using-asp-net-o
        function readURLFront(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#front_img').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

        function readURLBack(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#back_img').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

		$("#MainContent_front_upload").change(function () {
            readURLFront(this);
        });

		$(".#MainContent_back_upload").change(function () {
            readURLBack(this);
        });

	</script>

    <!-- Inline CSS section -->
    <style>
        .back_txtbx, .front_txtbx {
            width: 100%;
            padding: 12px 20px;
            margin: 8px 0;
            display: inline-block;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }
    </style>

    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <div id="MessagePanel">
                    <asp:Panel ID="message_pnl" CssClass="message_pnl" runat="server">
                        <a href="#" class="close hidden" data-dismiss="alert" aria-label="close">&times;</a>
                        <asp:Label ID="message_lbl" CssClass="message_lbl" runat="server" />
                    </asp:Panel>
                </div>

                <div class="well well-lg">
                    <asp:Label ID="category_lbl" runat="server">Category: </asp:Label>
                    <asp:DropDownList ID="category_dropdownlist" runat="server" CssClass="btn btn-default dropdown-toggle" DataTextField="CategoryName" DataValueField="Id" AppendDataBoundItems="true"></asp:DropDownList>

                    <button type="button" id="createCategory_btn" class="btn btn-warning" onclick="OpenPopup()"><i class="fa fa-plus" aria-hidden="true"> Create Category</i></button>
                </div>

                <div class="panel panel-default">
                    <div class="panel-body">
                        <!--<asp:Label ID="front_lbl" runat="server">Front</asp:Label>-->
                        <h4 class="text-left">Front</h4>
                        <asp:FileUpload ID="front_upload" runat="server" OnChange="readURLFront(this)" CssClass="btn btn-default" />
                        <img id="front_img" src="//:0" style="height: 150px; width: 290px" class="img-responsive" />
                        <br />
                        <asp:TextBox ID="front_txtbx" class="front_txtbx" runat="server" TextMode="MultiLine" placeholder="Enter text here."></asp:TextBox>
                    </div>
                </div>

                <div class="panel panel-default" runat="server">
                    <div class="panel-body" runat="server">
                        <!--<asp:Label ID="back_lbl" runat="server">Back</asp:Label>-->
                        <h4 class="text-left">Back</h4>
                        <asp:FileUpload ID="back_upload" runat="server" OnChange="readURLBack(this)" CssClass="btn btn-default" />
                        <img id="back_img" src="//:0" style="height: 150px; width: 290px" class="img-responsive" />
                        <br />
                        <asp:TextBox ID="back_txtbx" class="back_txtbx" runat="server" TextMode="MultiLine" placeholder="Enter text here."></asp:TextBox>
                    </div>
                </div>
                <asp:Button ID="create_btn" runat="server" CssClass="btn btn-primary" Text="Create Flashcard" OnClientClick="return ValidateForm()" OnClick="CreateFlashCard" UseSubmitBehavior="true" />
                <asp:Button ID="ButtonCancel" runat="server" CssClass="btn btn-warning" Text="Back" OnClick="RedirectToViewDashboard" />
            </div>
        </div>
    </div>
</asp:Content>
