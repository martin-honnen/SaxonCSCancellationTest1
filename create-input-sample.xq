declare namespace output = "http://www.w3.org/2010/xslt-xquery-serialization";

declare option output:method 'xml';
declare option output:indent 'yes';

declare variable $number-of-items as xs:integer external := 1000;

declare variable $levels as xs:integer external := 3;

declare function local:samples() as node()* {
  fold-right(1 to $levels, <foo>test</foo> , function($l, $a) {
    (1 to $number-of-items) ! <item>{$a}</item>
  })
};

document {
<root>
{
  local:samples()
}  
</root>
}