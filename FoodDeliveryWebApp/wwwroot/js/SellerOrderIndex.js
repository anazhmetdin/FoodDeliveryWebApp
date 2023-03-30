"use strict";

(() => {
	var connection = new signalR.HubConnectionBuilder().withUrl("/SellerOrdersIndexHub").build();

	$(function () {
		connection.start()
			.then(function () {
				InvokeOrders();
			})
			//.catch(function (err) {
			//	return console.error(err.toString());
			//})
	});

	// Order
	function InvokeOrders() {
		connection.invoke("SendOrders")
			//.catch(function (err) {
			//	return console.error(err.toString());
			//});
	}

	connection.on("ReceivedOrders", function (PostedOrders, InprogressOrders) {
		BindOrdersToGrid(PostedOrders, InprogressOrders);
	});

	function BindOrdersToGrid(PostedOrders, InprogressOrders) {
		var posted = $('#PostedOrders');
		var InProgress = $('#InProgressOrders');

		posted.html('');
		InProgress.html('');

		posted.html(PostedOrders)
		InProgress.html(InprogressOrders)
	}
})();