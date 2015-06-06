<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ItemWeb.Index" %>
<%@ Import Namespace="Items" %>
<%@ Import Namespace="ItemWeb" %>

<!DOCTYPE html>
<html lang="en">
	<head>
		<meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="viewport" content="width=device-width, initial-scale=1">
		<title>Model documentation</title>
		<link href="./css/bootstrap.min.css" rel="stylesheet">
		<!--[if lt IE 9]>
			<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
			<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
		<![endif]-->
		<style>
		</style>
	</head>
	<body>

		<div class="container">

			<div class="header">
				<h1 class="title"><a href="Index.aspx">Kitchen Model</a></h1>
				<p class="description">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras porttitor nunc in ligula aliquet venenatis. Vestibulum porta, ligula non vulputate egestas, dolor eros egestas odio, pharetra porta ante urna vitae lectus. Vivamus accumsan convallis erat, vel ullamcorper quam egestas vel. Pellentesque rutrum pulvinar mi euismod ullamcorper. Nulla vehicula elit purus. Fusce commodo tempus tempor. Pellentesque quis mi sit amet odio venenatis elementum. Suspendisse sed arcu non enim volutpat lacinia. Fusce sit amet gravida sapien. Nulla nec est a sapien maximus vestibulum. Pellentesque porttitor eget dolor eu aliquam.</p>
			</div>

			<div class="row">
				<div class="col-sm-4">
					<h2>Items</h2>
					<ul>
						<% foreach (Item item in Global.Model.Items.Values)
						{
							Response.Write(String.Format("<li><a href=\"./Item/{0}\">{0}</a></li>", item.Name));
						}
						%>
					</ul>
				</div>
				
				<div class="col-sm-4">
					<h2>Categories</h2>
					<ul>
						<% foreach (Category category in Global.Model.Categories.Values)
						{
							Response.Write(String.Format("<li><a href=\"./Category/{0}\">{0}</a></li>", category.Name));
						}
						%>
					</ul>
				</div>
					
				<div class="col-sm-4">
					<h2>Relationships</h2>
					<ul>
						<% foreach (Relationship relationship in Global.Model.Relationships.Values)
						{
							Response.Write(String.Format("<li><a href=\"./Relationship/{0}\">{0}</a></li>", relationship.Name));
						}
						%>
					</ul>
				</div>
			</div>

		</div>

		<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
		<script src="js/bootstrap.min.js"></script>

	</body>
</html>