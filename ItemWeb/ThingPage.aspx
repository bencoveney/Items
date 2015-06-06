﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThingPage.aspx.cs" Inherits="ItemWeb.ThingPage" %>
<%@ Import Namespace="Items" %>
<%@ Import Namespace="ItemWeb" %>

<!DOCTYPE html>
<html lang="en">
	<head>
		<meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="viewport" content="width=device-width, initial-scale=1">
		<title>Model documentation</title>
		<link href="../css/bootstrap.min.css" rel="stylesheet">
		<!--[if lt IE 9]>
			<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
			<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
		<![endif]-->
		<style>
			.description 
			{
				color: Gray;
			}
			.section
			{
				background-color: #
			}
		</style>
	</head>
	<body>

		<div class="container">

			<div class="page-header">
				<h1 class="title"><a href="../Index.aspx">Kitchen Model</a> <small>Generated using the Items library.</small></h1>
			</div>

			<div class="row">

				<div class="col-sm-8">
					<h2>
						<% Response.Write(this.Thing.Name); %> <small><% Response.Write(this.ThingType); %></small>
					</h2>

					<p>
						<% this.WriteDescription(); %>
					</p>

					<div class="section">
						<h3>Attributes</h3>

						<div class="row">
							<% foreach (DataMember attribute in this.Thing.Attributes)
							{
								WriteAttribute(attribute, this.Thing.IntegerIdentifier == attribute || this.Thing.StringIdentifier == attribute);
							}
							%>
						</div>
					</div>

					<div class="section">
						<h3>Relationships</h3>

						<div class="row">
							<% foreach (Relationship relationship in this.Thing.GetReferenceRelationships(Global.Model))
							{
								WriteRelationship(relationship);
							}
							%>
						</div>
					</div>
				</div>
			
				<div class="col-sm-3 col-sm-offset-1">
					<h2>Items</h2>
					<ul>
						<% foreach (Item item in Global.Model.Items.Values)
		 {
			 Response.Write(String.Format("<li><a href=\"../Item/{0}\">{0}</a></li>", item.Name));
		 }
						%>
					</ul>

					<h2>Categories</h2>
					<ul>
						<% foreach (Category category in Global.Model.Categories.Values)
		 {
			 Response.Write(String.Format("<li><a href=\"../Category/{0}\">{0}</a></li>", category.Name));
		 }
						%>
					</ul>

					<h2>Relationships</h2>
					<ul>
						<% foreach (Relationship relationship in Global.Model.Relationships.Values)
		 {
			 Response.Write(String.Format("<li><a href=\"../Relationship/{0}\">{0}</a></li>", relationship.Name));
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