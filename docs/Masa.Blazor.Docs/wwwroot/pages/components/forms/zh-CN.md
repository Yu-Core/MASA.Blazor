---
title: Forms（表单）
desc: "在表单验证方面，**MASA Blazor** 具有大量集成和内置功能。"
related:
  - /blazor/components/selects
  - /blazor/components/selection-controls
  - /blazor/components/text-fields
---

## 使用

内部的 **MForm** 组件可以很容易地向表单输入添加验证。所有输入组件都有一个 `Rules` 参数，该参数接受类型为 `Func<string?, StringBoolean>` 的列表。这允许您指定输入有效或无效的条件。每当输入值改变时，数组中的每个函数都会收到一个新值，并且每个数组元素都会被评分。如果函数或数组元素返回 `false` 或 `string`，表示验证失败，则将字符串值显示为错误消息。

<masa-example file="Examples.components.forms.Usage"></masa-example>

## 示例

### 属性

#### 规则

规则允许您对所有表单组件应用自定义验证。这些是按顺序验证的，一次最多显示 1 个错误，因此请确保相应地对规则进行排序。

<masa-example file="Examples.components.forms.Rules"></masa-example>

### 其他

#### 模型验证

除了在每个输入组件上通过 `Rules` 属性进行验证之外，还可以对单个对象模型进行验证。

<masa-example file="Examples.components.forms.Validation"></masa-example>

#### 验证单个字段 {released-on=v1.6.0}

使用**MForm**实例的`Validate`方法验证单个字段。

<masa-example file="Examples.components.forms.ValidateField"></masa-example>

#### 通过提交和清除进行验证 {updated-in=v1.6.0}

在 **MForm** 的内容里可以使用 `Context` 提供的方法，在 **MForm** 外部可以使用 `@ref` 拿到组件实例提供的方法。

<masa-example file="Examples.components.forms.ValidationWithSubmitAndClear"></masa-example>

#### 启用 I18n {updated-in=v1.6.0}

通过 `EnableI18n` 属性启用 [I18n](/blazor/features/internationalization) 以支持验证信息多语言。示例使用的本地话资源你能在 [GitHub](https://github.com/masastack/MASA.Blazor/blob/0f4a450479bceb816d58bbbb7b8f8ca7655e2f94/docs/Masa.Docs.Shared/wwwroot/locale/zh-CN.json#L129) 中找到。

<app-alert type="warning" content="无法作用于[复杂类型](#section-901a8fc7-dataannotations-9a8c8bc1590d67427c7b578b)，并且只支持索引为`0`的属性名的本地化，例如`[Range]`的错误信息可能不会被正确本地化。因此推荐使用FluentValidation或者使用额外的*Resources.resx*应用本地化。"></app-alert>

<masa-example file="Examples.components.forms.EnableI18n"></masa-example>

#### 通过 DataAnnotations 验证复杂类型

使用 `[ValidateComplexType]` 属性和 `<ObjectGraphDataAnnotationsValidator />`，可以验证复杂类型。

```xml Project.csproj
<PackageReference Include="Microsoft.AspNetCore.Components.DataAnnotations.Validation" Version="3.2.0-rc1.20223.4" />
```

<masa-example file="Examples.components.forms.ValidateComplexType"></masa-example>

#### 使用 FluentValidation 验证

**MForm** 支持 **FluentValidation** 验证。

<app-alert type="warning" content="验证器需要注册，详情请查看 [FluentValidation Dependency Injection](https://docs.fluentvalidation.net/en/latest/di.html)。"></app-alert>

<masa-example file="Examples.components.forms.ValidateWithFluentValidation"></masa-example>

#### 解析外部验证结果

**MForm** 支持解析 `ValidationResult`，用户可以将服务端表单验证返回的 `ValidationResult` 作为 `FormContext.ParseFormValidation` 的参数，将验证结果在前端表单展示,以验证集合为示例。

<masa-example file="Examples.components.forms.ParseFormValidation"></masa-example>
