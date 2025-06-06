---
title: Mobile pickers
desc: "A picker designed for the mobile. Provides multiple sets of options for users to choose, and supports single-column selection, multi-column selection and cascading selection."
related:
  - /blazor/mobiles/mobile-picker-views
  - /blazor/mobiles/mobile-date-time-views
  - /blazor/mobiles/mobile-time-pickers
---

## Installation {released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.MobileComponents
```

## Examples

### Props

#### Cascade

Use the cascading `Columns` and `ItemChildren` fields to achieve the effect of cascading options.

<app-alert type="warning" content="The data nesting depth of the cascade selection needs to be consistent, and if some of the options do not have sub
options, you can use an empty string for placeholder."></app-alert>

<masa-example file="Examples.mobiles.mobile_pickers.Cascade"></masa-example>

#### Custom item height

You can customize the height of the option through `Itemheight`. Currently, only `px` is supported.

<masa-example file="Examples.mobiles.mobile_pickers.ItemHeight"></masa-example>


