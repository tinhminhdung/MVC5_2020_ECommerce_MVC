/**
 * Look under your chair! console.log FOR EVERYONE!
 *
 * @see http://paulirish.com/2009/log-a-lightweight-wrapper-for-consolelog/
 */
(function(b){function c(){}for(var d="assert,count,debug,dir,dirxml,error,exception,group,groupCollapsed,groupEnd,info,log,timeStamp,profile,profileEnd,time,timeEnd,trace,warn".split(","),a;a=d.pop();){b[a]=b[a]||c}})((function(){try
{console.log();return window.console;}catch(err){return window.console={};}})());

/**
 * Page-specific call-backs
 * Called after dom has loaded.
 */
var GLOBAL = {
	common : {
		init: function(){
			$('.add_to_cart').bind( 'click', addToCart );
		}
	},

	templateIndex : {
		init: function(){

		}
	},

	templateProduct : {
		init: function(){

		}
	},

	templateCart : {
		init: function(){

		}
	}

}
var UTIL = {
	fire : function(func,funcname, args){
		var namespace = GLOBAL;
		funcname = (funcname === undefined) ? 'init' : funcname;
		if (func !== '' && namespace[func] && typeof namespace[func][funcname] == 'function'){
			namespace[func][funcname](args);
		}
	},

	loadEvents : function(){
		var bodyId = document.body.id;

		// hit up common first.
		UTIL.fire('common');

		// do all the classes too.
		$.each(document.body.className.split(/\s+/),function(i,classnm){
			UTIL.fire(classnm);
			UTIL.fire(classnm,bodyId);
		});
	}

};
$(document).ready(UTIL.loadEvents);
/**
 * Ajaxy add-to-cart
 */
Number.prototype.formatMoney = function(c, d, t){
	var n = this, 
		c = isNaN(c = Math.abs(c)) ? 2 : c, 
		d = d == undefined ? "." : d, 
		t = t == undefined ? "." : t, 
		s = n < 0 ? "-" : "", 
		i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "", 
		j = (j = i.length) > 3 ? j % 3 : 0;
	return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

function addToCartSuccess (jqXHR, textStatus, errorThrown){
	
	$.ajax({
		type: 'GET',
		url: '/cart.js',
		async: false,
		cache: false,
		dataType: 'json',
		success: function (cart){
			awe_hidePopup('.loading');
			Haravan.updateCartFromForm(cart, '.top-cart-content .mini-products-list');
			Haravan.updateCartPopupForm(cart, '#popup-cart-desktop .tbody-popup');
		}
	});
	
	
	
	var url_product = jqXHR['url'];
	var class_id = jqXHR['product_id'];
	var name = jqXHR['title'];
	var textDisplay = ('<i style="margin-right:5px; color:red; font-size:13px;" class="fa fa-check" aria-hidden="true"></i>Sản phẩm vừa thêm vào giỏ hàng');
	var id = jqXHR['variant_id'];
	var dataList = $(".item-name a").map(function() {
		var plus = $(this).text();
    	return plus;
	}).get();
	$('.title-popup-cart .cart-popup-name').html('<a href="'+ url_product +'" title="'+name+'">'+ name + '</a> ');
	var nameid = dataList,
		found = $.inArray(name, nameid);
	var textfind = found;
	
	var src = '';
	if(Haravan.resizeImage(jqXHR['image'], 'small') == null || Haravan.resizeImage(jqXHR['image'], 'small') =="null" || Haravan.resizeImage(jqXHR['image'], 'small') ==''){
		src= 'http://bizweb.dktcdn.net/thumb/large/assets/themes_support/noimage.gif'
	}
	else
	{
		src=  Haravan.resizeImage(jqXHR['image'], 'small')
	}
	
	$(".item-info > p:contains("+id+")").html('<span class="add_sus" style="color:#fe526a;"><i style="margin-right:5px; color:#fe526a; font-size:14px;" class="fa fa-check" aria-hidden="true"></i>Sản phẩm vừa thêm</span>');
	
	
	
	
	
	var windowW = $(window).width();
	if(windowW > 768){				
		$('#popup-cart').modal();
	}else{
		$('#myModal').html('');
		var $popupMobile = '<div class="modal-dialog"><div class="modal-content"><div class="modal-header">'
		+ '<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="position: relative; z-index: 9;"><span aria-hidden="true">×</span></button>'
		+ '<h4 class="modal-title"><span><i class="fa fa-check" aria-hidden="true"></i></span>Thêm vào giỏ hàng thành công</h4></div>'
		+ '<div class="modal-body"><div class="media"><div class="media-left"><div class="thumb-1x1">'
		+ '<img width="70px" src="'+ src +'" alt="'+ jqXHR['title'] +'"></div></div>'
		+ '<div class="media-body"><div class="product-title">'+ jqXHR['title'] +'</div>'
		+ '<div class="product-new-price"><span>' + (jqXHR['price']).formatMoney(0) + ' đ</span></div></div></div>'
		+ '<button class="btn btn-block btn-outline-red" data-dismiss="modal">Tiếp tục mua hàng</button>'
		+ '<a href="/checkout" class="btn btn-block btn-red">Tiến hành thanh toán »</a></div></div></div>';
		$('#myModal').html($popupMobile);
		$('#myModal').modal();
		clearTimeout($('#myModal').data('hideInterval'));
		$('#myModal').data('hideInterval', setTimeout(function(){
			$('#myModal').modal('hide');
		}, 5000));
	}
}
function addToCartFail(jqXHR, textStatus, errorThrown){
	var response = $.parseJSON(jqXHR.responseText);
	var $info = '<div class="error">'+ response.description +'</div>';
}
$(document).on('click', ".remove-item-cart", function () {
	var variantId = $(this).attr('data-id');
	removeItemCart(variantId);
});
$(document).on('click', ".items-count", function () {
	var thisBtn = $(this);
	var variantId = $(this).parent().find('.variantID').val();
	var qty =  $(this).parent().children('.number-sidebar').val();
	if (qty == '0') {
		$(this).parent().children('.number-sidebar').val(1);
	}
	updateQuantity(qty, variantId);
});
$(document).on('change', ".number-sidebar", function () {
	var variantId = $(this).parent().children('.variantID').val();
	var qty =  $(this).val();
	updateQuantity(qty, variantId);
});
function updateQuantity (qty, variantId){
	var variantIdUpdate = variantId;
	$.ajax({
		type: "POST",
		url: "/cart/update.js",
		data: 'quantity='+qty+'&id=' + variantId,
		dataType: "json",
		success: function (cart, variantId) {
			Haravan.onCartUpdateClick(cart, variantIdUpdate);
		},
		error: function (qty, variantId) {
			Haravan.onError(qty, variantId)
		}
	})
}
function removeItemCart (variantId){
	var variantIdRemove = variantId;
	$.ajax({
		type: "POST",
		url: "/cart/update.js",
		data: 'quantity=0&id=' + variantId,
		dataType: "json",
		success: function (cart, variantId) {
			Haravan.onCartRemoveClick(cart, variantIdRemove);
			$('.productid-'+variantIdRemove).remove();
			if($('.tbody-popup>div').length == '0' ){
				$('#popup-cart').modal('hide');
			}
			if($('.list-item-cart>li').length == '0' ){
				$('.mini-products-list').html('<div class="no-item"><p>Không có sản phẩm nào trong giỏ hàng.</p></div>');
			}
			if($('.cart-tbody>div').length == '0' ){
				$('.bg-cart-page').remove();
				$('.bg-cart-page-mobile').remove();
				jQuery('<div class="bg-cart-page" style="min-height: auto"><p>Không có sản phẩm nào trong giỏ hàng. Quay lại <a href="/">cửa hàng</a> để tiếp tục mua sắm.</p></div>').appendTo('.cart');
			}
		},
		error: function (variantId, r) {
			Haravan.onError(variantId, r)
		}
	})
}