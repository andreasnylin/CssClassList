# A CSS Class List Helper for ASP.NET

This is a small helper for working with CSS classes in ASP.NET. It is inspired by the HTML DOM APIs ClassList.

First a bit of background why I created this.

I often find code like this where string concatenation is used to create output the CSS classes of elements. This works but it is a bit hard to read and it is a lot of code for a simple task. Adding or changing a class would require some thought. It is also hard to tell if a certain class has been added to the list or if we have added a white space to separate the classes.

@{
	var cssClass = "menu-item";

	if(menuItem.IsActive) 
	{
		cssClass += " active";
	}

	if(menuItem.HasChildren) 
	{
		cssClass +=  " has-children";
	}

	cssClass += menuItem.Level == 1 ? " first-level" : " sub-level";
	
}
<li class="@cssClass"><a href="@menuItem.Url">@menuItem.Text</a></li>

---

A better, more clean aproach would be to use a list of strings to store the classes. It can be done this way. This is better because we don't have to keep track of any white space and we can easilly remove a value or check if the list contains a value. However it is still a lot of code.

@{
	var cssClass = new HashSet<string>() { "menu-item" };

	if(menuItem.IsActive) 
	{
		cssClass.Add("active");
	}

	if(menuItem.HasChildren) 
	{
		cssClass.Add("has-children");
	}

	if(menuItem.Level == 1) 
	{
		cssClass.Add("first-level");
	}
	else
	{
		cssClass.Add("sub-level");
	}	
}
<li class="string.Join(" ", cssClass)"><a href="@menuItem.Url">@menuItem.Text</a></li>

---

Here is the same code using the CSS Class List Helper. As you can see it requires less code.

@{
	var cssClass = new CssClassList("menu-item");
	cssClass.AddIf(menuItem.IsActive, "active");
	cssClass.AddIf(menuItem.HasChildren, "has-children");
	cssClass.AddIf(menuItem.Level == 1, "first-level", "sub-level");
}

<li class="@cssClass.ToString()"><a href="@menuItem.Url">@menuItem.Text</a></li>


var cssClasses = new CssClassList();

cssClasses.Add("apple");
cssClasses.Add("pear banana");
cssClasses.Add(new List<string>() { "peach", "plum" });
cssClasses.Remove("banana");
cssClasses.Remove(new List<string>() { "peach", "plum" });
cssClasses.AddIf(true, "kiwi");
cssClasses.AddIf(true, "lemon", "lime");
cssClasses.HasClass("lemon");
cssClasses.ToString();
cssClasses.RenderList();
// result => "apple pear kiwi lemon"
cssClasses.RenderClassAttribute();
// result => class="apple pear kiwi lemon"
