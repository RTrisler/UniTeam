using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseMenuEventHandler : MonoBehaviour
{
    /// <summary>
    /// Focus on the first element with the "first-focused" class.
    /// </summary>
    /// <param name="document"> The document to search for the element in</param>
    public virtual void FocusOnDefaultElement(UIDocument document)
    {
        // Focus on the first element with the "first-focused" class
        var firstFocused = document.rootVisualElement.
        Q<VisualElement>(className: "first-focused");                                       
        if (firstFocused != null)
        {
            firstFocused.Focus();
        }
        else
{
            Debug.LogError("No element with the \"first-focused\" class found");
        }
    }

    /// <summary>
    /// Show the initial menu when the menu is enabled.
    /// </summary>
    /// <remarks>
    /// Inital menu is determined by the <see cref="InitialMenuAttribute"/> on a property.
    /// </remarks>
    protected virtual void OnEnable()
    {
        FieldInfo initialMenu = this.GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
            .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(InitialMenuAttribute)));

        SwitchDocuments(initialMenu.Name);
    }

    /// <summary>
    /// Disable any shown menus, 
    /// enable the menu with the given name, 
    /// and focus on the document's default element.
    /// </summary>
    /// <param name="documentName">The variable name of the document being switched to.</param>
    protected abstract void SwitchDocuments(string documentName);

    protected abstract void DisableAllMenus();
}
