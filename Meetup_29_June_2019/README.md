# Code Walkthrough for binary classification problem using ML.Net ![img](https://upload.wikimedia.org/wikipedia/commons/thumb/0/02/Mldotnet.svg/480px-Mldotnet.svg.png)



## Problem: Predict the survival of traveler in Titanic ship tragedy

### Pre-requisites

- Visual Studio Code(Free IDE - https://code.visualstudio.com/download )/Visual Studio 2017 and above
- .Net Core SDK (Latest) : https://dotnet.microsoft.com/download
- ML.Net CLI : run 'dotnet tool install -g mlnet'



**Note:** Below instructions are for windows, however it can be used in a similar on MacOS and Linux. Just change the path as per OS.

#### Setup

1. Create a directory on system (c:\binaryclassification) 

2. Open Visual Studio code(VSCode) -> File -> Open Folder -> Navigate to above directory and select it

3. Select 'New Terminal' from 'Terminal' at the top menu. It should open a command prompt with VS Code

4. Let's create a solution first for our projects. Enter below commands in terminal window

   > dotnet new sln --name binaryclassificationdemo

5. Create project : A console application

   > dotnet new console -o titanicbinaryclassification

6. Navigate to titanicbinaryclassification directory

   > cd titanicbinaryclassification

7. Install nuget packages. In the terminal enter below commands

   > dotnet add package Microsoft.ML

8. Download titanic data from https://web.stanford.edu/class/archive/cs/cs109/cs109.1166/problem12.html

9. The data looks like below

| Survived | Pclass | Name                                                 | Sex    | Age  | Siblings Aboard | Parents Aboard | Fare    |
| -------- | ------ | ---------------------------------------------------- | ------ | ---- | --------------- | -------------- | ------- |
| 0        | 3      | Mr. Owen Harris Braund                               | male   | 22   | 1               | 0              | 7.25    |
| 1        | 1      | Mrs. John Bradley (Florence Briggs Thayer)   Cumings | female | 38   | 1               | 0              | 71.2833 |
| 1        | 3      | Miss. Laina Heikkinen                                | female | 26   | 0               | 0              | 7.925   |
| 1        | 1      | Mrs. Jacques Heath (Lily May Peel) Futrelle          | female | 35   | 1               | 0              | 53.1    |
| 0        | 3      | Mr. William Henry Allen                              | male   | 35   | 0               | 0              | 8.05    |

10. Create a directory 'data' and copy titanic.csv to it.

    > mkdir data

11. Open 'titanicbinaryclassification.csproj' in VSCode and copy below text just before </Project> tag

12. ```
    <ItemGroup>
    	<Content Include="data/titanic.csv"> 
        	<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
        </Content>
    </ItemGroup>
    ```

 

13. Navigate back to titanicbinaryclassification directory and create data schema 'Passenger.cs'

    > cd..
    >
    > mkdir Schema
    >
    > cd Schema
    >
    > type nul > Passenger.cs

14. Add below code to Passenger.cs

15. ```  c#
    using Microsoft.ML.Data;
    
    namespace BinaryClassificationDemo.Schema
    {
        public class Passenger
        {
            [LoadColumn(0)]
            public bool Survived { get; set; }
    
            [LoadColumn(1)]
            public float PClass { get; set; }
    
            [LoadColumn(2)]
            public string Name { get; set; }
    
            [LoadColumn(3)]
            public string Sex { get; set; }
    
            [LoadColumn(4)]
            public float Age { get; set; }
    
            [LoadColumn(5)]
            public float SiblingsAboard { get; set; }
    
            [LoadColumn(6)]
            public float ParentsAboard { get; set; }
        }
    }
    ```

16. 

