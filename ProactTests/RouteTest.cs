﻿using Proact.Core;
using Proact.Core.Tag;

namespace ProactTests;

public class RouteTest
{
    [Fact]
    public void IsMatched_matched_url_should_return_true()
    {
        var route = new Route("/about", new Div());

        var matchResult = route.GetMatchResult("/about");
        
        Assert.True(matchResult.IsMatched);
    }
    
    [Fact]
    public void IsMatched_unmatched_url_should_return_false()
    {
        var route = new Route("/about", new Div());

        var matchResult = route.GetMatchResult("/not-matching-url");
        
        Assert.False(matchResult.IsMatched);
    }
    
    [Fact]
    public void IsMatched_url_is_not_matched_if_something_is_extra()
    {
        var route = new Route("/about", new Div());

        var matchResult = route.GetMatchResult("/about/something-extra");
        
        Assert.False(matchResult.IsMatched);
    }
    
    [Fact]
    public void IsMatched_matching_url_with_query_parameters_should_return_true()
    {
        var route = new Route("/about", new Div());

        var matchResult = route.GetMatchResult("/about?myQueryParam=HelloWorld");
        
        Assert.True(matchResult.IsMatched);
    }
    
    
    [Fact]
    public void IsMatched_should_return_path_parameters()
    {
        var route = new Route("/page/{pageNumber}", new Div());

        var matchResult = route.GetMatchResult("/page/123");
        
        Assert.True(matchResult.IsMatched);
        Assert.Equal("123", matchResult.PathParameters["pageNumber"]);
    }
    
    
    [Fact]
    public void IsMatched_should_return_multiple_path_parameters()
    {
        var route = new Route("/page/{pageNumber}/{section}/{name}", new Div());

        var matchResult = route.GetMatchResult("/page/123/abc/philip");
        
        Assert.True(matchResult.IsMatched);
        Assert.Equal("123", matchResult.PathParameters["pageNumber"]);
        Assert.Equal("abc", matchResult.PathParameters["section"]);
        Assert.Equal("philip", matchResult.PathParameters["name"]);
    }
}