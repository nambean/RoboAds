# RoboAds from Farmer Team with ❤

Hello anh em, hôm nay rảnh ngồi gõ vài dòng chia sẻ cho anh em một tool về 1 số kiến thức cơ bản mà anh em có thể áp dụng vào hành trình làm tool MMO nhé, kkk
Mình dân kỹ thuật nên không giỏi nói hoa mỹ hay hàn lâm đâu, ae cố gắng tư duy thêm nhá

Ok, sơ bộ thế, chúng ta bắt đầu vào 1 ví dụ cụ thế ntn nhé
- Yêu cầu bài toán như thế này, khách hàng ( tạm gọi là Pig Boss ) đang có 1 request như thế này

Pig Boss giàu vcl, Pig Boss đang có nhiều BM và đang có nhiều nhân viên chạy ads thuê, mà cũng éo hiểu nguồn acc của Pig Boss đâu ra lắm thế, lâu nay cứ có nhóm chung tele để cho nvien vào lấy để chạy, mà giờ nv thì nhiều, Pig Boss sợ bọn này chạy ltinh hoặc mất BM thì bỏ mẹ ( dù Pig Boss giàu vcl r :(( ), thế này thuê mấy thằng IT lởm code cho cái tool quản lý BM. Mà éo hiểu Pig Boss là sếp kiểu gì mà sợ nhân viên nó biết Pig Boss quản lý nên Pig Boss lại yêu cầu mấy thằng IT lởm là t muốn kiểm soát trình duyệt bọn nhân viên xem bọn nó làm gì, có xem séc hay nói xấu t k + với t muốn lấy hết cookie fb của bno, xem bno dùng mấy con Via BM của t ntn. Đm, nghe đến đây thấy khó vcl, giờ máy tk méo nào chả lên win 10, win 11 sẵn antivirus r. Làm tool lấy mấy cái thông tin ltinh đã bị báo là trojan hay virus r, mà mình là dev chứ có phải hacker méo đâu, nhưng nghĩ đến vợ già con thơ cũng cố quay tay ra ỉ ôi, chém gió khó vcl Pig Boss ơi, thế là Pig Boss bơm thêm cho ít $ động viên, thế là vào việc thôi.

Đứng trên góc độ dev thì mình cũng éo hiểu chuyên sâu mấy cái hack hủng cho lắm, giờ làm thế méo nào vượt qua đc mấy cái antivirus nhỉ. À từ từ, vẽ tạm cho anh em cái ảnh cho dễ hình dung về chiến thuật ban đầu

![](https://share.sketchpad.app/24/b7a-fcea-1579cf.png)

Hehe, đơn giản vcl, mình lừa nó cài extension chrome hay cài app exe xong bú thôi, đơn giản vl

Nhưng đời đâu là mơ, extension get cookie thì ok, nhưng đẩy về server là oẳng chó ngay, dù đm nghĩ ra nhiều kế bọc link, đẩy qua bên thứ 3 xong mới đẩy về server cũng tầm hơn tuần là bị phát hiên, nghĩ tiếp ra quả obfuscator code đc tầm 15 ngày cũng hẹo, thật ra với quả obfuscator code này sống đc 15 ngày là thơm rồi, nhưng tiếc 5$ mua acc dev chrome nên thôi tìm phương án khác

Ngồi ngẫm nghĩ tiếp, à đm cái gì trust đc với windows nhỉ, mà mấy đứa văn phòng hay dùng nhất thì chỉ có Work, Excel, kkk. T biết code macro mà =)). Thế là bem thôi, 30p làm cái macro gán vào file excel ngon đét, upload hết profile trình duyệt về server của mình, mà lại éo bị antivirus nó báo gì cả, file thì gửi qua fb, zalo, tele thoải cmn mái, ngon r =)). Đm nhưng mà đời không là mơ, lên bản word, excel 2017 hay 2019 là có cảnh báo phải enable macro lên mới chạy đc :( cái này hơi đần, đéo chứng tỏ mình chuyên nghiệp gì cả, ta lại tìm cách khác, cho ae xem cái hình excel nó cảnh báo 

![](https://github.com/nambean/RoboAds/blob/main/Capture.PNG?raw=true)

Nằm ngẫm nghĩ thêm, thì bao đời nay dính virus hay cái cc gì đa số là toàn do cài mấy cái crack game hay cài phần mềm ltinh, thế có nghĩa application vẫn là chân ái, nhưng mà lại quay về là antivirus nó báo, mình cũng có phải hacker méo đâu mà biết vượt qua ntn, chẳng lẽ bảo ai cài thì tắt antivirus đi như mấy cái crack game à, thế này thì Pig Boss đấm vỡ mõm mình mất, tiền thì cầm tiêu r =)) thế lại ngồi ngẫm nghĩ tiếp ( Chỗ này anh em tư duy thêm nhé, kkk )

Đến đây rồi, thì để av nó không báo thì chỉ có 1 cách là làm thật, mình phải làm 1 phần mềm tử tế, nhưng thế thì nói làm con mie gì, giờ phải làm sao one click one kill ( client click vào file là toạch luôn chứ =)) ), chỗ này thì mình xin phép không giải thích sâu hơn về cách làm, mình chỉ gợi ý cho ae thế thôi, về bản chất ae phải tư duy về chiến lược của mình để phù hợp từng yêu cầu, từng kịch bản của khách hàng hay cá nhân đề ra, okay.

Ok, ngon rồi one click one shot đã xong, file cookie + info client về ầm ầm rồi.


Giờ đến bước tiếp theo, khi đã có được cookie rồi thì làm mịe gì đây nhỉ. Mở lên xem thì toàn bộ dữ liệu đã được mã hóa hết cmnr, giờ phải làm sao đây. Qua 1 hồi tìm hiểu thì đã google mã hóa dữ liệu trong cookie bằng giải thuật AES256-GCM. Nhưng đm key giải mã nó nằm ở đâu nhỉ. Phải stackoverflow thần chưởng thôi chứ còn mịe gì nữa. Sau bao nhiêu ngày lần mò trên đấy. Cuối cùng thì cũng đã có output, tất cả các trình duyệt nhân Chromium sẽ có 1 file Local State. Trong đây có rất nhiều cấu hình và 1 cái quan trọng nhất là key giải mã cái dữ liệu của cookie.

Và bùmmmm...., đã giải mã được hết tất cả các dữ liệu trong file cookie. Việc tiếp theo làm gì thì tự các đồng dâm xử lý nhé.

=> Việc này ok với tất cả trình duyệt sử dụng nhân chromium nhé

Tiếp tục sẽ đến phần Application ( phần này liên quan đến auto thao tác, đa luồng.... ), có time mình sẽ typing tiếp nhé
