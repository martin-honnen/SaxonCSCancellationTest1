declare namespace output = "http://www.w3.org/2010/xslt-xquery-serialization";

declare option output:method 'xml';
declare option output:indent 'yes';

declare variable $number-of-items as xs:integer external := 10000;


document {
<root>
  <items>
  {
  (1 to $number-of-items)!<item>value {format-integer(., '0000')}</item>
  }
  </items>
</root>
}