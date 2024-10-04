# Code convention

Unity & C# code convention. Everything is typed in the English language.

Always try to make your code less [coupled](https://en.wikipedia.org/wiki/Coupling_(computer_programming)) / dependant on other code, and try to adhere to [SRP (Single Responsability Princple)](https://en.wikipedia.org/wiki/Single-responsibility_principle) doing so will prevent scripts from breaking entire systems if something breaks.
Also try to make your code [DRY (Dont Repeat Yourself)](https://en.wikipedia.org/wiki/Don%27t_repeat_yourself#:~:text=%22Don't%20repeat%20yourself%22,data%20normalization%20to%20avoid%20redundancy.)

- [Namespaces](#namespaces)
    - [Importing namespaces](#importing-namespaces)
- [Classes](#classes)
- [Functions](#functions)
- [Variables](#variables)
    - [Hungarian notation](#hungarian-notation)
- [Structs](#structs)
- [Enums](#enums)
- [If statement](#if-statements)
    - [Ternary operator](#ternary-operator)
- [Loops](#loops)
- [Scriptable object](#scriptable-object)
- [Styling](#styling)
    - [Namespaces](#namespaces-style)
    - [Bracket Placement](#bracket-placement)
    - [If statement and loop](#if-statement-and-loop)
    - [Regions](#regions)

------
### Namespaces
The namespace name is written in PascalCasing. The lines in the namespaces should should not exstend a chracter count of 120.
Every class, scriptableObject and struct needs to be inside of a namespace.
```cs
namespace ExampleNamespace
{
    public class ExampleScript : MonoBehaviour
    {
        
    }
}
```
```cs
using UnityEngine;

namespace ExampleNamespace.ScriptableObjects
{
    public class ExampleScriptableObject : ScriptableObject
    {
        
    }
}
```
```cs
using Systems;

namespace ExampleNamespace
{
    public struct ExampleStruct
    {
        
    }
}
```

#### Importing namespaces
When using namespaces we put the default namespaces first then a white space followed by our namespaces.

```cs
using System;
using System.Collections;
using UnityEngine;

using FrameWork;
using FrameWork.Enums;
using FrameWork.Extensions;
using Player;
```

------
### Classes
The Class name is written in PascalCasing.
If the function GetComponent is used to get a component from this gameObject you use RequireComponent above the class. I suggest using the 'sealed' and 'abstract' keywords to minmize confusion.
```cs
[RequireComponent(typeof(ExampleComponent))]
public sealed class ExampleScript : MonoBehaviour
{

}

public abstract class BaseExampleScript : MonoBehaviour
{

}

public class NonBaseExampleScript : BaseExampleScript
{

}
```

Class members should be grouped into sections:

- Constant Fields
- Static Fields
- Fields
- Constructors
- Properties
- Events
- Delegates
- LifeCycle Methods (Awake, OnEnable, OnDisable, OnDestroy, IEnumerator)
- Public Methods
- Protected Methods
- Private Methods
- Nested types

Within each of these groups order by access:
- public
- serializedFields
- internal
- protected
- private

------
### Functions
All functions and events perform some form of action, whether its getting info, calculating data, or causing something to explode. Therefore, all functions should **start with verbs**. They should be worded in the present tense whenever possible. They should also have some context as to what they are doing.

When writing a function that does not change the state of or modify any object and is purely for getting information, state, or computing a yes/no value, it should ask a question. This should also follow the verb rule.

This is extremely important as if a question is not asked, it may be assumed that the function performs an action and is returning whether that action succeeded.

The Function name is written in PascalCasing.
```cs
private void ExampleFunction()
{
    
}
```

**Access modifiers** are always written with functions.
```cs
void ExampleFunction()
{
    Debug.Log("Not allowed");
}

private void ExampleFunction()
{
    Debug.Log("I'm a private function.");
}

protected void ExampleFunction()
{
    Debug.Log("I'm a protected function.");
}

public void ExampleFunction()
{
    Debug.Log("I'm a public function.");
}
```

**Public & protected functions** require a summary including the parameters and returns.
```cs
/// <summary>
/// Function description.
/// </summary>
/// <param name="parameter">Parameter value to pass.</param>
/// <returns>What the function return.</returns>
public int ExampleFunction(string parameter)  
{
    Return 0;
}

/// <summary>
/// Function description.
/// </summary>
protected void ExampleFunction()  
{
    Debug.log("I am example!");
}
```

When there is more than 2 parameter, we add this rule for readability. This does defeat the rule below.
```cs
// Good
private void ExampleFunction(int firstNumber, int secondNumber)
{
    
}

// Good
private void ExampleFunction(
    int firstNumber,
    int secondNumber,
    float numberWithComma,
    ExampleComponent targetClass,
    bool isTrue,
    double funnyNumber)
{
    
}

// Bad
private void ExampleFunction(int firstNumber, int secondNumber, float numberWithComma, ExampleComponent targetClass, bool isTrue, double funnyNumber)
{
    
}
```

When there is only 1 line of code inside a function you can use a lambda expression.

When the lambda expression is over the character limit of 120. You need to break up the code in local variables.
```cs
public void ExampleFunction() => SecondExampleFunction();

// with parameters
public void ExampleFunction(
    int firstNumber,
    int secondNumber,
    float numberWithComma,
    ExampleComponent targetClass,
    bool isTrue,
    double funnyNumber)
    => SecondExampleFunction();
```

------
### Variables
When writing a function that does not change the state of or modify any object and is purely for getting information, state, or computing a yes/no value, it should ask a question. This should also follow the verb rule.

This is extremely important as if a question is not asked, it may be assumed that the function performs an action and is returning whether that action succeeded.

A variable is almost always private. If you need the value make a getter for it. This is also why serialized have a '_' exception.

**Access modifiers** are always written with variables.
```cs
// Allowed
private int _variableExample0;
protected int p_variableExample1;
protected internal int pi_variableExample;
public int variableExample3;

// Not allowed
int _variableExample4;
```

**Private variable** names always start with an '_' (Except when serialized) after which it is written in camelCasing. If the variable is accessible in the **Unity Inspector**, and it's an int or float it needs the Range attribute.
```cs
private Object _variableExample;

[SerializeField] private Object secondVariableExample;

[SerializeField, Range(0, 10)] private int thirdVariableExample;

[SerializeField, Range(0, 1)] private float fourthVariableExample;
```

**Public variable** names are written in camelCasing. If not a number, char, string or bool, it needs to have the Tooltip attribute.
```cs
[Tooltip("Explaination of this varible.")] public Object variableExample;
```

**Readonly variable** names are written the same as public variables so in camelCasing.
```cs
public readonly Object variableExample;
```

**Constant variable** names are written in FULL_CAPITALS with snake_casing.
```cs
public const int EXAMPLE_CONSTANT_VALUE;
```

**Internal variable** names always start with 'i_' after which it is written in camelCasing.
```cs
internal int i_variableExample;
```

**Protected variable** names always start with 'p_' after which it is written in camelCasing.
```cs
protected int p_variableExample;
```

**Internal & protected  variable** names always start with 'pi_' after which it is written in camelCasing.
```cs
protected internal int pi_variableExample;
```

**Temporary variables** inside a function always need to be written out and are written in camelCasing.
```cs
private void ExampleFunction()
{
    float temporaryFloat = 1f;
    int temporaryInt = 1;
    double temporaryDouble = 1.00;
}
```

**Temporary constants** inside a function always need to be written out and are written in FULL_CAPITALS with snake_casing.
```cs
private void ExampleFunction()
{
    const float TEMPORARY_FLOAT = 1f;
    const int TEMPORARY_INT = 1;
    const double TEMPORARY_DOUBLE = 1.00;
}
```

**Property** names are written in PascalCasing.
```cs
public int ExampleInteger
{
    get => _exampleInterger;
    set 
    {
        if(value < 0) _exampleInterger = 0;
    }
}
```
#### Hungarian notation

We don't do that here. It's crucial to note that Hungarian notation is considered a suboptimal practice in coding standards.

```cs
// good
private int _targetAmount;
private ExampleComponent _system;
private ExampleStruct _currentStruct;

//bad
private int _intTargetAmount;
private ExampleComponent _exampleComponetSystem;
private ExampleStruct _exampleStructCurrentStruct;
```
This also apply to collections. They follow the same naming rules as mentioned before, but should be named as a plural noun.
```
// good
'Enemies', `Targets` and `Hats`

// bad
'DictionaryEnemies', `TargetList` and `HatArray`
```

------
### Structs
The struct name is written in PascalCasing and everything inside the struct follows the usual code conventions.
```cs
public struct ExampleStruct
{
    public double x;
    public double y;
}
```

------
### Enums
The enum name is written in PascalCasing while the constants are in FULL_CAPITALS with snake_casing.
```cs
enum ExampleEnum
{
    FIRST_CONSTANT,
    SECOND_CONSTANT
}
```
Always have the default type at the top. For example all food starts raw.

```cs
enum CookedState
{
    RAW,
    COOKED,
    BURNED
}
```

------
### If statements
When there is only 1 line of code after an if statement it comes right after it and same with the else.
```cs
if (_exampleBoolean)
    ExampleFunction();
else
    SeccondExampleFunction();

if (_exampleBoolean)
    return;
```

If either the if or the else in the statement contains multiple lines of code, the if and the else do not need brackets both.
```cs
if (_exampleBoolean)
    ExampleFunction();
else
{
    SeccondExampleFunction();
    ThirdExampleFunction();
}
```

When the condition has multiple conditions, make new lines for it.
```cs
// bad example
if (_exampleBoolean && 0 == 0 || true)
    ExampleFunction();

// good example
if (_exampleBoolean
    && 0 == 0
    || true)
    ExampleFunction();

// good example
if (_exampleBoolean && _otherExampleBoolean
    || true)
    ExampleFunction();

// also good example
bool canBeCalled = _exampleBoolean && 0 == 0 || true;
if (canBeCallled)
    ExampleFunction();
```

##### Ternary operator
I highly recommend ternary operators when dynamically change 1 variable. Also, a note, don't make them to big, no ternary operator in ternary operator.
```cs
// bad example
if (_exampleBoolean)
    _exampleFloat = 1;
else
    _exampleFloat = 69;

// good example
_exampleFloat = _exampleBoolean ? 1 : 69;
```

------
### Loops
For better performance (even very small) we make the length its own (local)variable.
```cs
int listLength = _exampleList.Length;

for (int i = 0; i < listLength; i++)
{

}
```

------
### Scriptable object
Scriptable objects holds data and/or settings, this needs to be reflected in the name. Do not forget the CreateAssetMenu attribute and put it in the correct namespace.
```cs
[CreateAssetMenu(fileName = "NewGunData", menuName = "Gun Data")]
public sealed class GunData : ScriptableObject
{
    private int _maxAmmoAmount;
    public int currentAmmoAmount;

    public void ResetAmmoAmount() => currentAmmoAmount = _maxAmmoAmount;
    public int MaxAmmoAmount() => _maxAmmoAmount;
}
```

Just in case, here is a bad way to make a scriptable object.
```cs
[CreateAssetMenu(fileName = "ScriptableObject / Song info")]
public sealed class SongInfo : ScriptableObject
{
    [field: SerializeField, Tooltip("This is the MP3 file that should play")] public AudioClip Song { get; private set; }
    [field: SerializeField, Tooltip("The person who made the song")] public string Artist { get; private set; }

    [Serializable] public struct LyricNode
    {
        [field: SerializeField, TextArea(5, 50)]  public string TextPart { get; private set; }
        [field: SerializeField, Tooltip("The delay till the next lyric node")] public float TimeStamp { get; private set; }
        [field: SerializeField, Range(0.05f, 1f)] public float Speed { get; private set; }
    }
    [field: SerializeField] public LyricNode[] Nodes { get; private set; }
}
```

------
### Styling
Code style is a personal preference. It is needed for a group project, so here is a style that we use.

Want to look at a [class with good code style](https://github.com/Team-Swamp/IceBites/blob/develop/Assets/Scripts/Framework/SceneSwitcher.cs)?

#### Namespaces style
There needs to be line between the namespaces in use and the current namespace. We also splits the standard namespaces and our namespaces.
```cs
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using Player;
using UI.Score;

namespace Framework 
{
    // example class
}
```

#### Bracket placement
A good usage of brackets:
```cs
namespace ExampleNamespace
{
    public class ExampleScript : MonoBehaviour
    {
        private void ExampleMethod()
        {

        }
    }
}
```
A bad usage of brackets:
```cs
namespace ExampleNamespace{
    public class ExampleScript : MonoBehaviour{
        private void ExampleMethod(){


}       }


}
```

#### If statement and loop
Around an if and loop there needs to be an empty line above and below.
```cs
float exampleFloat;

if (_exampleBoolean)
    exampleFloat = 1;

ExampleMethod(exampleFloat);
```
```cs
int listLength = _exampleList.Length;

for (int i = 0; i < listLength; i++)
{
    Debug.LogError($"Item {listLenght} is {_exampleList[listLength].name}.");
}

ExampleMethod();
```

#### Regions
A region has a line between its content.
```cs
#region Private variables

private int _targetAmount;
private ExampleComponent _system;
private ExampleStruct _currentStruct;

#endregion
```
```cs
#region Public functions

public void ExampleMethod()
{
    Debug.Log("Example")
}

#endregion
```

#### Functions
Have a line in between functions. This is how it should be done:
```cs
/// <summary>
/// Function description.
/// </summary>
/// <param name="parameter">Parameter value to pass.</param>
/// <returns>What the function return.</returns>
public int ExampleFunction(string parameter)  
{
    Return 0;
}

private void ExampleFunction()  
{
    Debug.log("I am example!");
}
```
Not like this:

```cs
/// <summary>
/// Function description.
/// </summary>
/// <param name="parameter">Parameter value to pass.</param>
/// <returns>What the function return.</returns>
public int ExampleFunction(string parameter)  
{
    Return 0;
}
private void ExampleFunction()  
{
    Debug.log("I am example!");
}
```
