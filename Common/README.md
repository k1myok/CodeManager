#C#、ASP.NET、ASP.NET MVC公共类整理
1. AuthorizeAttribute登录权限限制
	* 封装用户登录信息，[Login]用户只有登录才能访问，[AllowAnonymous]用户不需要登录既可以访问(MVC特性)
2. Dapper源码
	* SqlMapper(Dapper.Net源码研究)
3. Json对象转换
	* Json.NET、javaScriptSerializer对Json字符串转换成对象，对象转换成Json字符串
4. Linq扩展方法
	* 扩展OrderBy自定义排序,JOIN方法
5. MsOffice转化Pdf图片PDF转换成图片
	* 将Office系列软件(World)转换成PDF格式的文档和图片形式存放
6. MVC扩展方法
	* 封装分离控制器的实现类、将对象(主要是匿名对象)转换为View层可以访问的对象(dynamic),ViewBag调用
7. SMTP邮件发送
	* 封装发送邮件的公共类
8. Zip压缩通用类库
	* 对原始文件进行压缩(压缩文件，文件夹，流)以及解压缩
9. 操作Excel
	* 使用NPOI导出Excel,传递的集合是List对象(实体集合),类(ExcelBaseHelper、ExcelOneHelper)
10. 读取WebConfig
	* 读取WebConfig中AppSettings的信息和数据库连接字符串
11. 对象转化器
	* 实体对象转换List、List转换DataTable、实体对象转换为DataTable、DataRow转换实体对象
12. 缓存
	* HttpRuntimeCache、Memcache、Redis缓存封装类
13. 获取电脑信息
	* 读取电脑信息(IP、电脑名称、Mac地址)
14. 枚举_下拉框(Enum)
	* 读取枚举值heDescription属性，将枚举值存放到下拉框中实现
15. 通用类
	* EncryptHelper 各种加密/伪解密        StatusCode 系统中出现的错误枚举
	* StringRegexHelper 对字符串验证的操作(正则表达式,规范)
	* StringToolsHelper 对字符串进行扩展,判断对象是否是值/引用类型
16. 文件处理
	* FileBaseHelper:文件是否存在、查找文件中匹配的内容、文件加密解密
	* ImageHelper:图片的各种处理(缩略图、水印等等)-秦效甫提供。
17. 文件上传
	* 使用Html5 FormData封装的一个上传插件,内部含有html+css+js文件
18. 验证码
	* 网站中一般验证码(数字、字母、数字和字母的组合)的实现