<%@ Page Title="Sign in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signin.aspx.cs" Inherits="Flashminder.Signin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-4 col-md-4">
                <h3>Welcome!</h3>
                <asp:Label ID="LabelField" runat="server">Please enter username & password here.</asp:Label>
                <br />
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h2 class="panel-title"><i class="fa fa-refresh fa-spin"></i> Sign In</h2>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="contorl-label" for="form-group-input">Username</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="UserNameTextBox" Placeholder="Username" ></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ops! something looks incorrect." ControlToValidate="UsernameTextBox" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="form-label" for="form-group-input">Password</label>
                            <asp:TextBox runat="server" type="password" class="form-control" ID="PasswordTextBox" Placeholder="Password" />
                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Ops! something looks incorrect." ControlToValidate="PasswordTextBox" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                        <div class="text-left">
                            <asp:Button runat="server" ID="SendButton" CssClass="btn btn-primary" Text="Send" OnClick="SendButton_Click"/>
                            <asp:Button runat="server" UseSubmitBehavior="false" CausesValidation="false" ID="CancelButton" CssClass="btn btn-warning" Text="Cancel" OnClick="CancelButton_Click" />                             
                        </div>
                        <div class="label">
                            <a href="Signup.aspx">Register account?</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
