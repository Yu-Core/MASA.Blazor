﻿---
title: Pull refresh
desc: "Pull down to refresh, usually used in mobile apps"
---

<app-alert type="warning" content="Only works on mobile, because only implement touch events. So you need to open the browser's mobile mode(`F12`, `Ctrl+Shift+M`) to see the effect."></app-alert>

## Installation {released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.MobileComponents
```

## Usage

<masa-example file="Examples.mobiles.pull_refresh.Usage"></masa-example>

## Examples

### Props

#### Disabled

<masa-example file="Examples.mobiles.pull_refresh.Disabled"></masa-example>

#### On error

<masa-example file="Examples.mobiles.pull_refresh.OnError"></masa-example>

### Contents

#### Custom contents

<masa-example file="Examples.mobiles.pull_refresh.Contents"></masa-example>
