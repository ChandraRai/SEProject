<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditFlashcard.aspx.cs" Inherits="Flashminder.EditFlashcard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
				if (!$(".front_txtbx").val() && !$(".front_upload").val()) {
					ShowMessage("Front of the card needs text or an image", "warning");
					ret = false;
				}
				if ((!$(".back_txtbx").val() && !$(".back_upload").val())) {
					ShowMessage("Back of the card needs text or an image", "warning");
					ret = false;
				}

				if ($(".front_txtbx").val() && $(".front_txtbx").val().length > 500) {
					ShowMessage("Text can only be 500 characters", "warning");
					ret = false;
				}
				if ($(".back_txtbx").val() && $(".back_txtbx").val().length > 500) {
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

			$(".front_upload").change(function () {
				readURLFront(this);
			});

			$(".back_upload").change(function () {
				readURLBack(this);
			});


		</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="container">
        <div class="row">
            <div class="col-md-offset-2  col-md-6">
                <div id="MessagePanel" style="padding-bottom:25px">
                    <asp:Panel ID="message_pnl" CssClass="message_pnl" runat="server" >
                        <a href="#" class ="close hidden" data-dismiss="alert" aria-label="close">&times;</a>
                        <asp:Label ID="message_lbl" CssClass="message_lbl" runat="server" />
                    </asp:Panel>
                </div>
                <div>
                    <asp:Label ID="category_lbl" runat="server">Category: </asp:Label>
                    <asp:DropDownList ID="category_dropdownlist" runat="server" DataTextField="CategoryName" DataValueField="Id" AppendDataBoundItems="true" ></asp:DropDownList>
                    <button type="button" ID="createCategory_btn" OnClick="OpenPopup()">Create Category</button>
                </div>
                <div>
                    <asp:Label ID="front_lbl" runat="server">Front</asp:Label>
					<asp:Panel ID="front_img_change_pnl" runat="server" Visible="false">
						<asp:FileUpload id="front_upload" CssClass="front_upload" runat="server" OnChange="readURLFront(this)"/>
						<img id="front_img" src="//:0" style="height:100px;width:200px"/>
					</asp:Panel>
					<asp:Button id="front_img_change_btn" runat="server" Text="Change Image" OnClick="ShowChangeFrontPanel"/>
					<asp:Button id="remove_front_img_btn" runat="server" Text="Remove Image" OnClick="RemoveFrontImage" OnClientClick="return confirm('Are you sure you want to delete the image? This cannot be undone')"/>
					<p>Current Front Image:</p>
					<asp:Image id="current_front_img" runat="server"/>
                    <br />
                    <asp:TextBox ID="front_txtbx" CssClass="front_txtbx" runat="server" TextMode="MultiLine" placeholder="Enter text here..."></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="back_lbl" runat="server">Back</asp:Label>
					<asp:Panel ID="back_img_change_pnl" runat="server" Visible="false">
						<asp:FileUpload id="back_upload" CssClass="back_upload" runat="server" OnChange="readURLBack(this)"/>
						<img id="back_img" src="//:0" style="height:100px;width:200px"/>
					</asp:Panel>
					<asp:Button id="back_img_change_btn" runat="server" Text="Change Image" OnClick="ShowChangeBackPanel"/>
					<asp:Button id="remove_back_img_btn" runat="server" Text="Remove Image" OnClick="RemoveBackImage" OnClientClick="return confirm('Are you sure you want to delete the image? This cannot be undone')"/>
					<p>Current Back Image:</p>
					<asp:Image id="current_back_img" runat="server"/>
                    <br />
                    <asp:TextBox ID="back_txtbx" CssClass="back_txtbx" runat="server" TextMode="MultiLine" placeholder="Enter text here..."></asp:TextBox>
                </div>
                <asp:Button ID="update_btn" runat="server" Text="Update Flashcard" OnClientClick="return ValidateForm()" OnClick="UpdateFlashcard" UseSubmitBehavior="true" />
            </div>
        </div>
    </div>
</asp:Content>
