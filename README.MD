## 介绍：通过反射自动注入所有要依赖注入类

使用参考：DependencyInjectionDemo.WebApi

Program 类添加

``` 
var assembly = Assembly.GetAssembly(typeof(Program));
var serviceRegister = new DefaultConventionalRegistrar();
serviceRegister.AddAssembly(builder.Services, assembly!);

```

``` 
 var basePath = AppDomain.CurrentDomain.BaseDirectory;
 string path = Path.Combine(basePath, "当前解决方案名称");
 var assemblies = Directory.GetFiles(basePath, "*.dll").Where(x=> !x.Contains("Microsoft.") && !x.Contains("System.") && x.StartsWith(path)).Select(Assembly.LoadFrom).ToList();
 foreach (var assembly in assemblies)
 {
    var assembly = Assembly.GetAssembly(typeof(Program));
    var serviceRegister = new DefaultConventionalRegistrar();
    serviceRegister.AddAssembly(builder.Services, assembly!);
 }
```