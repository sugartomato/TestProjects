let verticalArr = [];
let service_charge_rule = service_charge_info ? service_charge_info.rule : [];

function getChargePrice(price) {
	let _price = 0;
	for(let _i = 0; _i < service_charge_rule.length; _i++) {
		if(service_charge_rule[_i].min <= price.price && service_charge_rule[_i].max >= price.price) {

			_price = service_charge_rule[_i].price;
			break;
		}
	}
	return _price;
}

$(window).on("load",function(){
	let touchType = ('createTouch' in document) ? 'tap' : 'click';
	let selectCourts = {}, uParam = {}, selectCourts2 = {}, bindCourtArr = [];
	let utils = {
		getUrlParam: function(){
			var s = location.search;
			if(s.length < 2) {
				return;
			}
			s = s.substr(1);
			var arr = s.split('&');
			$.each(arr, function(i,v){
				var n = v.split('=');
				uParam[n[0]] = n[1];
			});
		},
		updateSum: function(){
			let sum = 0;//折扣价
			let service_charge_price = 0; //服务费
			let usual = 0;//原价

			$.each(selectCourts, function(i,v){
				let service = getChargePrice(v);

				if (v.discount == 100){
					//js精度丢失问题
					sum = (Math.round(v.price * 1000) + Math.round(sum * 1000)) / 1000;
				}else {
					if (is_service){
						sum += Math.ceil((v.price-service) * (v.discount/100))+service;
					}else {
						sum += Math.ceil(v.price * (v.discount/100));

					}
				}

				usual = (Math.round(v.price * 1000) + Math.round(usual * 1000)) / 1000;
				service_charge_price += service;
			});

            var cent = '';
            var str = '';
            $('#select_court_info').html('');

            var sortCourtArr = utils.sortBy1(selectCourts2);
            sortCourtArr.sort(utils.sortBy2('sort',false,parseInt));

            sortCourtArr.forEach(function(item){
            	var contents = selectCourts2[item.id].split(",");
				cent +='<li><span>'+contents[1] + ' ' + contents[0] +'</span>';
                cent +='<input type="hidden" value="'+contents[2]+'" name="price[]" />';
                cent +='<input type="hidden" value="'+contents[1]+'" name="hour[]" />';
                cent +='<input type="hidden" value="'+contents[0]+'" name="course_name[]" /></li>';
                cent += '<input type="hidden" value="'+contents[3]+'" name="real_time[]" />';
                var checkReturn = utils.bindCourt(item.id,contents);
				if(!utils.checkInGroup(item.id)){
					var classN = "";
					classN = contents[0].length <= 6 ? "txtStyle3" : "txtStyle1";
					str += "<li><div>"+contents[3]+"</div><div><div class='"+classN+"'>"+contents[0]+"</div></div></li>"
				}else{
					if(checkReturn){
						str += checkReturn;
					}
				}
            });

			// let total = sum ? sum : "0";
			$('.j_total_fee').html(sum);
			if (usual != sum){
				$('.j_total_usual').html(usual);
				$('.book-usual-amount').attr('style','display:true');
				$('.book-amount-tip').attr('style','display:true');
			}else {
				$('.book-usual-amount').attr('style','display:none');
				$('.book-amount-tip').attr('style','display:none');
			}

			$('.j_handle_fee').html(service_charge_price);
			$('.J_submit div:nth-of-type(1)').html("提交订单");
            $('.court-tips ul').html(cent);
            $('.book-orderinfo').html(str);

            if($('.book-orderinfo li').size()>0){
            	$(".book-orderinfo").removeClass('hide');
            	$(".book-tip").addClass('hide');
            }else{
				$('.J_submit div:nth-of-type(1)').html("请选择场地");
            	$(".book-orderinfo").addClass('hide');
            	$(".book-tip").removeClass('hide');
            }
		},
		checkInGroup:function(id){
			var bInBindGroup = false;
			bindCourtArr.forEach(function(item){
				if(item.group_id.indexOf(id) > -1){
					bInBindGroup = true;
				}
			})
			return bInBindGroup;
		},
		bindCourt:function(id,contents){
			var returnStr = "";
			bindCourtArr.forEach(function(item){
				if(item.group_id.indexOf(id)>-1){
					if(item.key == 0){
						item.key = 1;
						returnStr = "";
					}else{
						item.key = 0;
						var arr = item.timeLen.split(",");
						    a = '',
						    b = arr[0].split("-")[0],
						    c = arr[0].split("-")[1],
						    d = arr[1].split("-")[0],
						    e = arr[1].split("-")[1];
						if(parseInt(b) > parseInt(d)){
							a = d + "-" +c;
						}else{
							a = b + "-" +e;
						}
						returnStr = "<li><div>"+a+"</div><div><div class='txtStyle2'>"+contents[0]+"</div><p>打包时段</p></div></li>";
					}
				}
			})
			return returnStr;
		},
		sortBy1:function(data) {
			var arr = [];
	        for (var i in data) {
	            arr.push({'id':i,'sort':data[i].split(",")[4]});
	        }
	        return arr;
	    },
		sortBy2:function(filed, rev, primer) {
	        rev = (rev) ? -1 : 1;
	        return function(a, b) {
	            a = a[filed];
	            b = b[filed];
	            if (typeof(primer) != 'undefined') {
	                a = primer(a);
	                b = primer(b);
	            }
	            if (a <= b) {
	                return rev * -1;
	            }
	            if (a > b) {
	                return rev * 1;
	            }
	            return 0;
	        }
	    }
	};

	let bookIndex = 0;
	let bindDOM = function(){
		$('.book-list').on("click", 'li', function(){
			if(g.bNopay > 0){
				$(".book-noPaySprite").removeClass("hide");
				return;
			}
			let el = $(this);
			let curGid = el.attr('goodsid');
			if(el.hasClass('disable')){
				showToast("该场次已出售");
				return;
			}

			verticalArr = [];
			//exist in the array, delete it
			if (selectCourts[curGid] != undefined) {
				delete selectCourts[curGid];
                delete selectCourts2[curGid];
				el.addClass('available');
				el.removeClass('selected');

				//最小起订时间限制
				if(minHour>1){
					let bindId = el.attr('bind_id'); //打包关联ID
					let col = el.parent();
					$('li[bind_id='+bindId+']').each(function(){
						let goodsId = $(this).attr('goodsid');
						delete selectCourts[goodsId];
                        delete selectCourts2[goodsId];
        				$(this).addClass('available');
        				$(this).removeClass('selected');
					});
				}else{
					//打包处理
	                let group_ids = el.attr('group_ids');

	                bindCourtArr = $.grep(bindCourtArr,function(item){
			            return (item.group_id != group_ids);
			        });

	                let group_arr = new Array();
	                if (group_ids != ''){
	                    group_arr=group_ids.split(',');
	                    for(let i=0;i<group_arr.length;i++){
	                        let blid = "#block_"+group_arr[i];
	                        if(!$(blid).hasClass('disable')  && !!$(blid)[0]){
	                            delete selectCourts[group_arr[i]];
	                            delete selectCourts2[group_arr[i]];
	            				$(blid).addClass('available');
	            				$(blid).removeClass('selected');
	                        }
	                    }  
	                }

				}
                
                //打包处理
			} else {
			    let selectNum=1;
                $.each(selectCourts, function(i,v){
    				selectNum++;
    			});
                if (selectNum > 4){
                    showToast("您选择的场次数太多啦，请分两次下单结算哦。");
                    return;
                }
		        bookIndex++;



				let currentIndex = el.index();
				let enableDown = true;
				let enableUp = true;
				let col = el.parent();
   				//最小起订时间限制
   				if(minHour>1){

					//判断上下时段是否符合选择
					for(let i=1;i<minHour;i++){
						if(!col.find('li').eq(currentIndex+i).hasClass('available')) enableDown = false;
						if(!col.find('li').eq(currentIndex-i).hasClass('available')) enableUp = false;
					}
					if(enableDown==false && enableUp==false){
						showToast('不足两小时，无法预订');
						return;
					}
					//自动选择相邻的场地
					let bindId = 'bind_'+curGid;
					let block = '#block_'+curGid;
					el.attr('bind_id',bindId);//设置打包关联ID


					for (let i = 0; i < minHour; i++) {
						// let nextInd = enableDown == true ? currentIndex + i : currentIndex - i;//设置方向往上或往下
						let nextInd = enableDown == true ? currentIndex + i : currentIndex - i;//设置方向往上或往下

						let nextTarget = col.find('li').eq(nextInd);
						let goodsId = nextTarget.attr('goodsid');

						selectCourts[goodsId] = {
							'price': Number(nextTarget.find("em").html()) || Number($(this).attr('price')),
							'discount': Number($(this).attr('discount'))
							// 'price': parseInt(nextTarget.find("em").html()) || parseInt($(this).attr('price')),
							// 'discount': parseInt($(this).attr('discount'))
							// 'price': parseInt(nextTarget.selector.attributes),
							// 'discount': parseInt($(this).attr('discount'))

						};
						// selectCourts[goodsId] = parseInt(nextTarget.find("em").html())|| parseInt($(this).attr('price'));

						selectCourts2[goodsId] = nextTarget.attr('course_content') + "," + bookIndex;
						nextTarget.removeClass('available');
						nextTarget.addClass('selected');
						nextTarget.attr('bind_id', bindId);
					}
					
   				}else{
   					//打包处理
					let target = col.find('li').eq(currentIndex);
					let group_ids = target.attr('group_ids');
   	                let group_arr = [];

   	                if (group_ids != ''){
   	                    group_arr=group_ids.split(',');
		           		let jsonObj = {group_id:group_ids,key:0};
   	           			let timeStr = "";
   	           			let bDisable = false;
   	           			let bindCourtNum = 0;
   	                    for(let i=0;i<group_arr.length;i++){
   	                        let blid = "#block_"+group_arr[i];
   	                        if(!$(blid).hasClass('disable') && !!$(blid)[0]){
   	                            selectCourts[group_arr[i]] = {
   	                            	// 'price':parseInt($(blid).find("em").html())|| parseInt($(this).attr('price')),
									// 'discount':parseInt($(this).attr('discount'))
									'price':Number($(blid).attr('price')),
									'discount':Number($(blid).attr('discount'))
									// 'price':parseInt($(blid).attr('price')),
									// 'discount':parseInt($(blid).attr('discount'))
   	                            };

   	                            selectCourts2[group_arr[i]] = $(blid).attr('course_content')+","+bookIndex;
   	           				    $(blid).removeClass('available');
   	           				    $(blid).addClass('selected');
   	           				    timeStr += (timeStr ? "," : "")+$(blid).attr('course_content').split(",")[3];
   	           				    bindCourtNum++;
   	                        }else{
   	                        	bDisable = true;
   	                        }
   	                    }
   	                    if(!bDisable || bindCourtNum >= 2){
   	                    	jsonObj.timeLen = timeStr;
			           		bindCourtArr.push(jsonObj);
   	                    }
   	                }else {
						let goodsId = target.attr('goodsid');

						selectCourts[goodsId] = {
							'price': Number(target.find("em").html()) || Number($(this).attr('price')),
							'discount': Number($(this).attr('discount'))
							// 'price': parseInt(target.find("em").html()) || parseInt($(this).attr('price')),
							// 'discount': parseInt($(this).attr('discount'))
						};

						selectCourts2[goodsId] = target.attr('course_content') + "," + bookIndex;
						target.removeClass('available');
						target.addClass('selected');
					}

   				}

   				// selectCourts[curGid] = parseInt(el.find("em").html()) || parseInt($(this).attr('price'));
                selectCourts2[curGid] = el.attr('course_content')+","+bookIndex;
   				el.removeClass('available');
				el.addClass('selected');
			} 

			$(".book-list ul").each(function(i,item){
				$(".book-area li").eq(i).removeClass("active");
				let ul = $(item);
				$("li",ul).each(function(j,val){
					let li = $(val);
					if(li.hasClass("selected")){
						if(!$(".book-area li").eq(i).hasClass("active")){
							$(".book-area li").eq(i).addClass("active");
						}
						verticalArr.push(j);
					}
				})
			});

			$(".book-time li").removeClass("active");
			for(let k in verticalArr){
				$(".book-time li").eq(verticalArr[k]).addClass("active");
				$(".book-time li").eq(verticalArr[k]+1).addClass("active");
			}
			utils.updateSum();

		});
		
		$('.day-wrap').on(touchType, '.J_selectDay', function(){
			let el = $(this);
			utils.getCourtData({
				datetime: parseInt(el.attr('time'))
			});
		});

		let submitLock = false;
		$('.J_submit').on("click", function(e){
			if(submitLock) return;

			e.stopPropagation();
			e.preventDefault();
			if(g.bNopay > 0){
				$(".book-noPaySprite").removeClass("hide");
				return;
			}
            let gids = [];
		    $.each(selectCourts, function(i,v){
  				gids.push(i);
   			});

            if (gids.length > 0){
				submitLock = true;
            	$("#loading").removeClass('hide');
                //跳转确认页面
                $('.J_goodsIds').val(gids.join(','));
                $('.J_payConfirm').submit();
                setTimeout(function(){
                	submitLock = false;
            		$("#loading").addClass('hide');
                },5000)
            }else{
                showToast("请选择场次");
            }
		});
        
        $('.J_submit2').on(touchType, function(){
			let gids = [];
			$.each(selectCourts, function(i,v){
				gids.push(i);
			});
			let postData = {
				goods_ids: gids.join(',')
			};
          postData = typeof objMerge == 'function' ? objMerge(postData) : postData;

		  $.ajax({
				url: '/order/doconfirm',
				type: 'GET',
				dataType: 'JSON',
				cache: false,
				data: postData,
				success: function(res){
                    var json=JSON.parse(res);
					if(json && json.code == 1){
						//todo: modify the url
						let url = '/order/pay?id='+json.data;
            // location.href = '/order/pay?id='+json.data;
						location.href = typeof urlAddParams == 'function' ? urlAddParams(url) : url;
					} else {
						utils.showError(res.msg || '支付出错，请稍后再试试');
					}
				},
				error: function(res){
					utils.showError(res.msg || '支付出错，请稍后再试试');
				}
			});
		});
	};

	utils.getUrlParam();
	bindDOM();

	$('.J_selectDay.active').trigger(touchType);
})