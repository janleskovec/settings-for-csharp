# settings-for-csharp
A simple text-based system to store settings and user preferences for C#.

### Usage:

```csharp
Settings.SetValue("key", "value");
string value = Settings.GetValue("key");
```

### Syntax:

```
key=value
```

### Additional notes:
* All newline and '=' characters in keys are replaced by '_'.
* All newline characters in values are converted to \n.
