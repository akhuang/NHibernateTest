继承：
1.单表实现:Teacher和OfficeUser，都继承自Employee，存储在Employee表中。
2.具体表实现：Student和Teacher都继承自Person，但是Person字段各自存储在自己的表中。
3.类表实现：Lab和Classroom都继承自Place，分别对应了Lab表，Classroom表和Place表。


关系：
1.一对一：作为班主任，一个Teacher对应一个Class，一个Class只有一个Teacher。
2.一对多：班级和学生，一个Class有多个Student，一个Student只对应一个Class。
3.多对多：学生选课，一个Student对应多个Course，一个Course对应多个Student。


NHibernate些特性：
1.用户对象（枚举）：
2.组件:Monetary
3.版本:Student对象中添加Version。