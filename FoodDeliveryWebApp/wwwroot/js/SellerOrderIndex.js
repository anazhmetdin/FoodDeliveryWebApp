"use strict";

(() => {
	const connection = new signalR.HubConnectionBuilder().withUrl("/SellerOrdersIndexHub").build();
	const notification = new Audio("/audio/magical_pop.mp3");
	const postedTab = $('#PostedOrders-tab');
	let first = true, OrdersCount = 0;

	$(function () {
		connection.start()
			.then(function () {
				InvokeOrders();
			})
			//.catch(function (err) {
			//	return console.error(err.toString());
			//})
	});

	postedTab.click((e) => { postedTab.addClass('nav-link') });

	connection.on("ReceivedOrders", function (PostedOrders, InprogressOrders, PostedCount) {
		BindOrdersToGrid(PostedOrders, InprogressOrders);
		BindAcceptButtons();

		if (first || OrdersCount >= PostedCount) {
			first = false;
		}
		else {
			postedTab.removeClass('nav-link');
			notification.play();
		}

		OrdersCount = PostedCount;

	});

	function BindOrdersToGrid(PostedOrders, InprogressOrders) {
		var posted = $('#PostedOrders');
		var InProgress = $('#InProgressOrders');

		posted.html('');
		InProgress.html('');

		posted.html(PostedOrders)
		InProgress.html(InprogressOrders)
	}

	function BindAcceptButtons() {

		$('#Orders-tab .order-button').click((e) => {
			const url = e.target.formAction;

			if (url.match("Rejected")) {
				if (confirm("Are you sure you want to cancel this order? this can't be undone"))
					fetch(url);
			}
			else {
				fetch(url);
			}

		})
	};
})();