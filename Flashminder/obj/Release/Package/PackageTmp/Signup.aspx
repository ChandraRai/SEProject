<%@ Page Title="Sign up" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Flashminder.Signup" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-4 col-md-4">
                <h3>Sign Up!</h3>

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h2 class="panel-title">All fields are required.</h2>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="contorl-label" for="form-group-input">Username</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtUsername" Placeholder="Username"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username is required." ControlToValidate="txtUsername" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="contorl-label" for="form-group-input">Email</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" Placeholder="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Email is required." ControlToValidate="txtEmail" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a valid email." ControlToValidate="txtEmail" CssClass="requiredFieldValidateStyle" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic">
                    </asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label class="contorl-label" for="form-group-input">Password</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" Placeholder="Password" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password is required." ControlToValidate="txtPassword" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label class="contorl-label" for="form-group-input">Confirm</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtConfirm" Placeholder="Confirm" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Confirm is required." ControlToValidate="txtConfirm" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                        <div class="text-left">
                            <asp:Button runat="server" ID="RegisterButton" CssClass="btn btn-primary" Text="Register" OnClick="RegisterButton_Click" />     
                            <asp:Button runat="server" UseSubmitBehavior="false" CausesValidation="false" ID="CancelButton" CssClass="btn btn-warning" Text="Cancel" OnClick="CancelButton_Click" />                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
