"use strict";

(() => {
	const notification = new Audio("/audio/magical_pop.mp3");
	const postedTab = $('#PostedOrders-tab');
	let OrdersCount = 0;

	BindAcceptButtons();

	$(function () {
		connection.start();
			//.then(function () {
			//	InvokeOrders();
			//})
			//.catch(function (err) {
			//	return console.error(err.toString());
			//})

		OrdersCount = $(".accept").length;
	});

	postedTab.click((e) => { postedTab.addClass('nav-link') });

	connection.on("ReceivedOrders", function (PostedOrders, InprogressOrders, PostedCount) {
		BindOrdersToGrid(PostedOrders, InprogressOrders);
		BindAcceptButtons();

		if (OrdersCount < PostedCount) {
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

	window.onunload = function () {
		connection.stop();
	}
})();